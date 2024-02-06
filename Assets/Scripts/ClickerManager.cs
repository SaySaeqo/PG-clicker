using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public static UnityEvent<int, int> OnItemBought = new UnityEvent<int, int>();

    [Header("Click Managment")]
    public ClickerUI clickerUI;
    public Button clickerButton;
    public Button resetButton;
    public Button lecturerTabButton;
    public Button upgradeTabButton;


    [SerializeField] public List<ShopUpgradeUI> upgrades;


    public GameObject door;
    public LecturerTabUI lecturerTab;

    [Header("Student Spawner")]
    public GameObject[] student;
    
    private double globalStudentMultiplier = 1;

    private Dictionary<string, double> lecturersMultipliers = new Dictionary<string, double>();

    private int studentCounterAvalange = 0;

    private int studentCounter = 0;
    private int studentsPerSecond = 0;
    private int socialMoney = 0;

    [Header("Lecturers and Upgrades Panel")]
    [SerializeField] private List<Lecturer> lecturers;

    private void Start()
    {
        clickerUI.UpdateStudentCounter(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
        upgradeTabButton.onClick.AddListener(clickerUI.ToggleTabs);
        lecturerTabButton.onClick.AddListener(clickerUI.ToggleTabs);

        foreach(Lecturer lecturer in lecturers)
        {
            lecturerTab.AddLecturer(lecturer);
        }
        upgrades.ForEach(upgrade => upgrade.buyButton.onClick.AddListener(() => BuyUpgrade(upgrade)));
        InvokeRepeating("FailStudentsAvalage", 10.0f, 10.0f);
        OnItemBought.AddListener(BuyItem);
        InvokeRepeating(nameof(AddStudentsPerSecond), 0f, 1f);
    }

    private void AddStudentsPerSecond()
    {
        if (studentsPerSecond > 0)
        {
            studentCounter += studentsPerSecond;
            clickerUI.UpdateStudentCounter(studentCounter);
        }
    }

    private void BuyItem(int price, int power)
    {
        if(price <= studentCounter)
        {
            studentsPerSecond += power;
            studentCounter -= price;
            clickerUI.UpdateStudentCounter(studentCounter);
        }
    }

    private void failStudent()
    {
        studentCounter += (int)Math.Ceiling(globalStudentMultiplier);
        clickerUI.UpdateStudentCounter(studentCounter);

        var doorPosition = door.transform.position;
        var MAX_DISTANCE_FROM_DOOR = 1f;
        var randX = doorPosition.x + UnityEngine.Random.Range(-MAX_DISTANCE_FROM_DOOR, MAX_DISTANCE_FROM_DOOR);
        var randY = doorPosition.y + UnityEngine.Random.Range(-MAX_DISTANCE_FROM_DOOR, MAX_DISTANCE_FROM_DOOR);

        int randSpawn = UnityEngine.Random.Range(0, student.Length);
        var created = Instantiate(student[randSpawn], new Vector2(randX, randY), Quaternion.identity);
        var RIGHT = 180;
        created.transform.Rotate(new Vector3(0,randX > doorPosition.x ? RIGHT : 0,0));
    }

    void FailStudentsAvalage()
    {
        if (studentCounterAvalange > 0)
        {
            studentCounter += (int)Math.Floor((double)studentCounter / studentCounterAvalange);
            clickerUI.UpdateStudentCounter(studentCounter);
        }
    }

    private void resetGame()
    {
        var x = studentCounter;
        const int MIN_STUDENTS = 0;
        const int STUDENTS_PER_CASH = 10;
        int y = (x-MIN_STUDENTS) / STUDENTS_PER_CASH;
        socialMoney += Math.Max(y, 0);
        studentCounter = 0;
        clickerUI.UpdateStudentCounter(studentCounter);
        clickerUI.UpdateCashCounter(socialMoney);
    }

    void BuyUpgrade(ShopUpgradeUI upgradeUI)
    {
        if (socialMoney < upgradeUI.cost) return;
        socialMoney -= upgradeUI.cost;
        upgradeUI.Buy();
        upgradeUI.cost *= 2;
        upgradeUI.UpdateDescriptionText();
        clickerUI.UpdateCashCounter(socialMoney);

        switch (upgradeUI.name)
        {
            case "Super komputer":
                globalStudentMultiplier *= 1.5;
                break;
            case "Jaskinia Lebiedzia":
                lecturersMultipliers["Lebiedz"] = 5.0;
                break;
            case "Doktoranci":
                studentCounterAvalange = 100;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
