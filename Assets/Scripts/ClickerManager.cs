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

        float randX = UnityEngine.Random.Range(3f, 7f);
        float randY = UnityEngine.Random.Range(-2f, 1f);
        int randSpawn = UnityEngine.Random.Range(0, student.Length);
        Instantiate(student[randSpawn], new Vector2(randX, randY), Quaternion.identity);
    }

    private void resetGame()
    {
        var x = studentCounter;
        var y = x / 100 - 100;
        socialMoney += Math.Max(y, 0);
        studentCounter = 0;
        clikcerUI.UpdateStudentCounter(studentCounter);
        clikcerUI.UpdateCashCounter(socialMoney);
    }
}
