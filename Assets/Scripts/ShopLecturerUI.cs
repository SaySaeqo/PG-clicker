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
    private int price;

    private string lecturerName;
    
    public void refreshName()
    {
        if (GameState.Instance.lecturersMultipliers.ContainsKey(lecturerName))
        {
            int count = (int)GameState.Instance.lecturersMultipliers[lecturerName];
            lecturerTitle.text = lecturerName + " x " + count.ToString();
        }
        else
        {
            lecturerTitle.text = lecturerName;
        }
    }

    public void UpdateUI(Lecturer lecturer)
    {
        lecturerFace.sprite = lecturer.face;
        lecturerTitle.text = lecturerName = lecturer.lecturerName;
        lecturerPower.text = "Moc: " + lecturer.power.ToString();
        lecturerCost.text = "Koszt: " + lecturer.price.ToString();
        price = lecturer.price;

        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Wynajmij";
        buyButton.onClick.AddListener(delegate { UpgradeLecturer(lecturer.lecturerName); });
    }

    private void UpgradeLecturer(string name)
    {
        isHired = isHired || price <= GameState.Instance.studentCounter;
        ClickerManager.OnItemBought?.Invoke(name);
        if (isHired == true)
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Awansuj";
            refreshName();
        }
        
    }
}
