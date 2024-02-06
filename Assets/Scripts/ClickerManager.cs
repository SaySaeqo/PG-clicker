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

    public GameObject door;

    [Header("Student Spawner")]
    public GameObject[] student;

    private int studentCounter = 0;
    private int socialMoney = 0;

    private void Start()
    {
        clikcerUI.UpdateStudentCounter(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
        upgradeTabButton.onClick.AddListener(clikcerUI.ToggleTabs);
        lecturerTabButton.onClick.AddListener(clikcerUI.ToggleTabs);
    }

    private void failStudent()
    {
        studentCounter++;
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
}
