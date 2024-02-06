using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    [Header("Click Managment")]
    public ClikcerUI clikcerUI;
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
    private int socialMoney = 0;

    [Header("Lecturers and Upgrades Panel")]
    [SerializeField] private List<Lecturer> lecturers;

    private void Start()
    {
        clikcerUI.UpdateStudentCounter(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
        upgradeTabButton.onClick.AddListener(clikcerUI.ToggleTabs);
        lecturerTabButton.onClick.AddListener(clikcerUI.ToggleTabs);

        foreach(Lecturer lecturer in lecturers)
        {
            lecturerTab.AddLecturer(lecturer);
        }
        upgrades.ForEach(upgrade => upgrade.buyButton.onClick.AddListener(() => BuyUpgrade(upgrade)));
        InvokeRepeating("FailStudentsAvalage", 10.0f, 10.0f);
    }
    
    void Update()
    {

    }

    private void failStudent()
    {
        studentCounter += (int)Math.Ceiling(globalStudentMultiplier);
        clikcerUI.UpdateStudentCounter(studentCounter);

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
            clikcerUI.UpdateStudentCounter(studentCounter);
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
        clikcerUI.UpdateStudentCounter(studentCounter);
        clikcerUI.UpdateCashCounter(socialMoney);
    }

    void BuyUpgrade(ShopUpgradeUI upgradeUI)
    {
        if (socialMoney < upgradeUI.cost) return;
        socialMoney -= upgradeUI.cost;
        upgradeUI.Buy();
        upgradeUI.cost *= 2;
        upgradeUI.UpdateDescriptionText();

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
