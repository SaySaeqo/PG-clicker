using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ClikcerUI : MonoBehaviour
{
    public TextMeshProUGUI studentCounterText;
    public TextMeshProUGUI cashCounterText;

    public void UpdateStudentCounter(int amount)
    {
        studentCounterText.text = $"<sprite=\"student_sad_icon\" index=0>x{amount}";
    }

    public void UpdateCashCounter(int amount)
    {
        cashCounterText.text = $"<sprite=\"cash\" index=0>x{amount}";
    }
}
