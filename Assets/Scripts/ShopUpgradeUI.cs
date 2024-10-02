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

    private Upgrade upgrade;
    public void Buy(string name){
        if (GameState.Instance.socialMoney < upgrade.price) return;
        buyButton.interactable = false;
        buyButtonImage.enabled = true;
        ClickerManager.OnUpgradeBought?.Invoke(name);
        print("Kupiono upgrade: " + name);
    }

    public void UpdateUI(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        face.sprite = upgrade.face;
        title.text = upgrade.upgradeName;
        description.text = upgrade.descripton;
        price.text = "Koszt: " + upgrade.price.ToString();
        buyButton.onClick.AddListener(delegate { Buy(upgrade.upgradeName); });
    }
    
}
