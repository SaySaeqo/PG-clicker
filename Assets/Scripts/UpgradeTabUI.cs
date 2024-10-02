using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTabUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform content;

    [Header("Lecturers")]
    [SerializeField] private ShopUpgradeUI shopUpgradeUI;

    public void Add(Upgrade upgrade)
    {
        var newUpgrade = Instantiate(shopUpgradeUI, content);
        newUpgrade.UpdateUI(upgrade);
    }
}
