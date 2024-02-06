using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        lecturerPower.text = lecturer.power.ToString();
        lecturerCost.text = lecturer.price.ToString();
    }
}
