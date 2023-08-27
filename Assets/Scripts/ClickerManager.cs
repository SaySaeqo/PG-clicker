using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public ClikcerUI clikcerUI;
    public Button clickerButton;

    private int studentCounter = 0;

    private void Start()
    {
        clikcerUI.UpdateUI(studentCounter);
        clickerButton.onClick.AddListener(failStudent);
    }

    private void failStudent()
    {
        studentCounter++;
        clikcerUI.UpdateUI(studentCounter);
    }
}
