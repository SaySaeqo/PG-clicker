using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public static UnityEvent<int, int> OnItemBought = new UnityEvent<int, int>();
    public static UnityEvent<string> OnUpgradeBought = new UnityEvent<string>();

    [Header("Click Managment")]
    public ClickerUI clickerUI;
    public Button clickerButton;
    public Button resetButton;
    public Button lecturerTabButton;
    public Button upgradeTabButton;

    public GameObject door;
    public LecturerTabUI lecturerTab;
    public UpgradeTabUI upgradeTab;

    [Header("Student Spawner")]
    public GameObject[] student;

    [Header("Lecturers and Upgrades Panel")]
    [SerializeField] private List<Lecturer> lecturers;
    [SerializeField] private List<Upgrade> upgrades;
    private float lecturersWidth;

    private GameState state => GameState.Instance;

    private void Start()
    {
        clickerUI.UpdateStudentCounter(state.studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
        upgradeTabButton.onClick.AddListener(clickerUI.ToggleTabs);
        lecturerTabButton.onClick.AddListener(clickerUI.ToggleTabs);
        foreach (Lecturer lecturer in lecturers)
        {
            lecturerTab.AddLecturer(lecturer);
        }
        foreach (Upgrade upgrade in upgrades)
        {
            upgradeTab.Add(upgrade);
        }
        InvokeRepeating("FailStudentsAvalange", 10.0f, 10.0f);
        OnItemBought.AddListener(BuyItem);
        OnUpgradeBought.AddListener(BuyUpgrade);
        InvokeRepeating(nameof(AddStudentsPerSecond), 0f, 1f);
    }

    private void AddStudentsPerSecond()
    {
        if (state.studentsPerSecond > 0)
        {
            state.studentCounter += state.studentsPerSecond;
            clickerUI.UpdateStudentCounter(state.studentCounter);
        }
    }

    private void BuyItem(int price, int power)
    {
        if(price <= state.studentCounter)
        {
            state.studentsPerSecond += power;
            state.studentCounter -= price;
            clickerUI.UpdateStudentCounter(state.studentCounter);
        }
    }

    private void failStudent()
    {
        state.studentCounter += (int)Math.Ceiling(state.globalStudentMultiplier);
        clickerUI.UpdateStudentCounter(state.studentCounter);

        var doorPosition = door.transform.position;
        var MAX_DISTANCE_FROM_DOOR = 1f;
        var randX = doorPosition.x + UnityEngine.Random.Range(-MAX_DISTANCE_FROM_DOOR, MAX_DISTANCE_FROM_DOOR);
        var randY = doorPosition.y + UnityEngine.Random.Range(-MAX_DISTANCE_FROM_DOOR, MAX_DISTANCE_FROM_DOOR);

        int randSpawn = UnityEngine.Random.Range(0, student.Length);
        var created = Instantiate(student[randSpawn], new Vector2(randX, randY), Quaternion.identity);
        var RIGHT = 180;
        created.transform.Rotate(new Vector3(0,randX > doorPosition.x ? RIGHT : 0,0));
    }

    void FailStudentsAvalange()
    {
        if (state.studentCounterAvalange > 0)
        {
            state.studentCounter += (int)Math.Floor((double)state.studentCounter / state.studentCounterAvalange);
            clickerUI.UpdateStudentCounter(state.studentCounter);
        }
    }

    private void resetGame()
    {
        var x = state.studentCounter;
        const int MIN_STUDENTS = 0;
        const int STUDENTS_PER_CASH = 10;
        int y = (x-MIN_STUDENTS) / STUDENTS_PER_CASH;
        state.socialMoney += Math.Max(y, 0);
        state.studentCounter = 0;
        state.studentsPerSecond = 0;
        clickerUI.UpdateStudentCounter(state.studentCounter);
        clickerUI.UpdateCashCounter(state.socialMoney);
    }

    void BuyUpgrade(string name)
    {
        var upgrade = upgrades.Find(u => u.name == name);
        if (state.socialMoney < upgrade.price) return;
        state.socialMoney -= upgrade.price;

        switch (name)
        {
            case "Super komputer":
                state.globalStudentMultiplier *= 1.5;
                break;
            case "Jaskinia Lebiedzia":
                state.lecturersMultipliers["Lebiedz"] = 5.0;
                break;
            case "Doktoranci":
                state.studentCounterAvalange = 100;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
