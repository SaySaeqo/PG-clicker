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

    public Texture2D student;

    private int studentCounter = 0;
    private int socialMoney = 0;

    private void Start()
    {
        clikcerUI.UpdateStudentCounter(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        resetButton.onClick.AddListener(resetGame);
    }

    private void failStudent()
    {
        studentCounter++;
        clikcerUI.UpdateStudentCounter(studentCounter);
        Instantiate(student, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }

    private void resetGame()
    {
        var result = (Math.Pow(2, studentCounter) - 1000) / 1000;
        socialMoney += (int)Math.Floor(result);
        studentCounter = 0;
        clikcerUI.UpdateStudentCounter(studentCounter);
        clikcerUI.UpdateCashCounter(socialMoney);
    }
}
