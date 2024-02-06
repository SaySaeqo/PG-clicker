using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ClickerUI : MonoBehaviour
{
    public TextMeshProUGUI studentCounterText;
    public TextMeshProUGUI cashCounterText;
    public Image upgradeTab;
    public Image lecturerTab;

    public void UpdateStudentCounter(int amount)
    {
        studentCounterText.text = $"<sprite=\"student_sad_icon\" index=0>x{amount}";
    }

    public void UpdateCashCounter(int amount)
    {
        cashCounterText.text = $"<sprite=\"cash\" index=0>x{amount}";
    }

    public void ToggleTabs()
    {
        var currentUpgradeTabStatus = upgradeTab.gameObject.activeSelf;
        upgradeTab.gameObject.SetActive(!currentUpgradeTabStatus);
        lecturerTab.gameObject.SetActive(currentUpgradeTabStatus);
    }
}
