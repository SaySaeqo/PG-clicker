using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopUpgradeUI : MonoBehaviour
{
    [SerializeField] private Image face;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image buyButtonImage;

    private Upgrade Upgrade;
    public void Buy(string name){
        ClickerManager.OnUpgradeBought?.Invoke(name);
        print("Kupiono upgrade: " + name);
    }

    public void UpdateUI(Upgrade upgrade)
    {
        upgrade = upgrade;
        face.sprite = upgrade.face;
        title.text = upgrade.name;
        description.text = upgrade.descripton;
        price.text = "Koszt: " + upgrade.price.ToString();
        buyButton.onClick.AddListener(delegate { Buy(upgrade.name); });
    }
    
}
