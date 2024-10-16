using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public static UnityEvent<string> OnItemBought = new UnityEvent<string>();
    public static UnityEvent<string> OnUpgradeBought = new UnityEvent<string>();

    [Header("Click Managment")]
    public ClickerUI clickerUI;
    public Button clickerButton;
    public Button resetButton;
    public Button lecturerTabButton;
    public Button upgradeTabButton;

    private int secondsTillStartOfGame = 0;

    public GameObject door;
    public LecturerTabUI lecturerTab;
    public UpgradeTabUI upgradeTab;

    [Header("Student Spawner")]
    public GameObject[] student;

    [Header("Lecturers and Upgrades Panel")]
    [SerializeField] private List<Lecturer> lecturers;
    [SerializeField] private List<Upgrade> upgrades;
    private float lecturersWidth;

    [SerializeField] private TextMeshProUGUI gameResult;

    private GameState state => GameState.Instance;

    private void Start()
    {
        clickerUI.UpdateStudentCounter(state.studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
        upgradeTabButton.onClick.AddListener(clickerUI.ToggleTabs);
        lecturerTabButton.onClick.AddListener(clickerUI.ToggleTabs);
        lecturers.Sort();
        upgrades.Sort();
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
        secondsTillStartOfGame += 1;
        if (state.studentsPerSecond > 0)
        {
            state.studentCounter += (int)(state.studentsPerSecond * state.globalStudentMultiplier);
            clickerUI.UpdateStudentCounter(state.studentCounter);
        }
    }

    private void BuyItem(string name)
    {
        var lecturer = lecturers.Find(l => l.lecturerName == name);
        if(lecturer.price <= state.studentCounter)
        {
            state.studentCounter -= lecturer.price;
            if (state.boughtLecturers.ContainsKey(lecturer.lecturerName))
            {
                if (state.lecturersMultipliers.ContainsKey(lecturer.lecturerName))
                {
                    state.lecturersMultipliers[lecturer.lecturerName] = state.lecturersMultipliers[lecturer.lecturerName] + 1.0;
                }
                else
                {
                    state.lecturersMultipliers.Add(lecturer.lecturerName, 2.0);
                }
            }
            else
            {
                state.boughtLecturers.Add(lecturer.lecturerName, lecturer.power);
            }
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
        state.boughtLecturers = new Dictionary<string, int>();
        state.lecturersMultipliers = new Dictionary<string, double>();
        lecturerTab.resetMultiplayers();
        clickerUI.UpdateStudentCounter(state.studentCounter);
        clickerUI.UpdateCashCounter(state.socialMoney);
    }

    void BuyUpgrade(string name)
    {
        var upgrade = upgrades.Find(u => u.upgradeName == name);
        if (state.socialMoney < upgrade.price) return;
        state.socialMoney -= upgrade.price;
        clickerUI.UpdateCashCounter(state.socialMoney);

        switch (name)
        {
            case "Super komputer":
                state.globalStudentMultiplier *= 1.5;
                break;
            case "Jaskinia Lebiedzia":
                if (state.lecturersMultipliers.ContainsKey("Lebiedź"))
                {
                    state.lecturersMultipliers["Lebiedź"] = state.lecturersMultipliers["Lebiedź"] * 5.0;
                }
                else
                {
                    state.lecturersMultipliers.Add("Lebiedź", 5.0);
                }
                lecturerTab.resetMultiplayers();
                break;
            case "Doktoranci":
                state.studentCounterAvalange = 100;
                break;
            case "Zły humor rektora":
                state.studentCounter += 100;
                clickerUI.UpdateStudentCounter(state.studentCounter);
                break;
            case "Meteor":
                state.studentCounter += 1000_000;
                clickerUI.UpdateStudentCounter(state.studentCounter);
                int hours = secondsTillStartOfGame / 3600;
                int mins = (secondsTillStartOfGame % 3600) / 60;
                int secs = (secondsTillStartOfGame % 60);
                gameResult.text = "You won in " + hours + "h " + mins + "min " + secs + "s!\nCongratulations !!!";
                break;
            default:
                throw new ArgumentOutOfRangeException("Działanie upgradu niezdefiniowane");
        }
    }
}
