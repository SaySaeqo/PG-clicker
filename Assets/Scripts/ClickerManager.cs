using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public ClikcerUI clikcerUI;
    public Button clickerButton;

    public Texture2D student;

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

        Instantiate(student, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
