using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopUpgradeUI : MonoBehaviour
{

    public Button buyButton;

    public Image buyButtonImage;

    public int cost;

    public string name;
    public string descripton;

    public TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDescriptionText();
        buyButtonImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDescriptionText(){
        descriptionText.text = name + "\n" + descripton + "\n" + "Cost: " + cost;
    }

    public void Buy(){
        buyButtonImage.enabled = true;
        buyButton.interactable = false;
    }
}
