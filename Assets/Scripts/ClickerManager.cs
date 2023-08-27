using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public ClikcerUI clikcerUI;
    public Button clickerButton;
    public Button resetButton;
    public Button lecturerTabButton;
    public Button upgradeTabButton;

    public Texture2D student;

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
        
        //Instantiate(student, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
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
