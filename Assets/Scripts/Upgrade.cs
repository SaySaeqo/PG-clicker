using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Clicker/New upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("General")]
    public Sprite face;
    public string upgradeName;

    public string descripton;
    public int price;

}
