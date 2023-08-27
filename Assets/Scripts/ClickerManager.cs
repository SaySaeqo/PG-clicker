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

    private int studentCounter = 0;
    private int socialMoney = 0;

    private void Start()
    {
        clikcerUI.UpdateStudentCounter(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
        clickerButton.onClick.AddListener(resetGame);
    }

    private void failStudent()
    {
        studentCounter++;
        clikcerUI.UpdateStudentCounter(studentCounter);
    }

    private void resetGame()
    {
        socialMoney += (int)Math.Log(studentCounter);
        studentCounter = 0;
    }
}
