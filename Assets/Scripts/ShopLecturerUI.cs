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

    public void UpdateUI(Lecturer lecturer)
    {
        lecturerFace.sprite = lecturer.face;
        lecturerTitle.text = lecturer.lecturerName;
        lecturerPower.text = "Moc: " + lecturer.power.ToString();
        lecturerCost.text = "Koszt: " + lecturer.price.ToString();

        buyButton.onClick.AddListener(delegate { UpgradeLecturer(lecturer.price, lecturer.power); });
    }

    private void UpgradeLecturer(int price, int power)
    {
        ClickerManager.OnItemBought?.Invoke(price, power);
    }
}
