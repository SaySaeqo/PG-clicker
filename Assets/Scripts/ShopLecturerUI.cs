using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopLecturerUI : MonoBehaviour
{
    [SerializeField] private Image lecturerFace;
    [SerializeField] private TextMeshProUGUI lecturerTitle;
    [SerializeField] private TextMeshProUGUI lecturerPower;
    [SerializeField] private TextMeshProUGUI lecturerCost;
    [SerializeField] private Button buyButton;
    private bool isHired = false;
    private Lecturer lecturer;
    
    public void refreshTitle()
    {
        if (GameState.Instance.lecturersMultipliers.ContainsKey(lecturer.lecturerName))
        {
            int count = (int)GameState.Instance.lecturersMultipliers[lecturer.lecturerName];
            lecturerTitle.text = lecturer.lecturerName + " x " + count.ToString();
        }
        else
        {
            lecturerTitle.text = lecturer.lecturerName;
        }
    }

    public void UpdateUI(Lecturer lecturer)
    {
        this.lecturer = lecturer;
        lecturerFace.sprite = lecturer.face;
        lecturerTitle.text = lecturer.lecturerName;
        lecturerPower.text = "Moc: " + lecturer.power.ToString();
        lecturerCost.text = "Koszt: " + lecturer.price.ToString();

        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Wynajmij";
        buyButton.onClick.AddListener(delegate { UpgradeLecturer(lecturer.lecturerName); });
    }

    private void UpgradeLecturer(string name)
    {
        isHired = isHired || lecturer.price <= GameState.Instance.studentCounter;
        ClickerManager.OnItemBought?.Invoke(name);
        if (isHired == true)
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Awansuj";
            refreshTitle();
        }
        
    }
}
