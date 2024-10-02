using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabUI : MonoBehaviour
{
    [SerializeField] private RectTransform content;

    [Header("Lecturers")]
    [SerializeField] private ShopUpgradeUI shopUpgradeUI;

    public void Add(Upgrade upgrade)
    {
        var newUpgrade = Instantiate(shopUpgradeUI, content);
        newUpgrade.UpdateUI(upgrade);
    }
}
