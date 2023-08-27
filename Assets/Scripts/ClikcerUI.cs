using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClikcerUI : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    public void UpdateUI(int amount)
    {
        counterText.text = $"X {amount}";
    }
}
