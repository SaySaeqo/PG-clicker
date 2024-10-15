using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Clicker/New upgrade")]
public class Upgrade : ScriptableObject, IComparable
{
    [Header("General")]
    public Sprite face;
    public string upgradeName;

    public string descripton;
    public int price;
    
    public int CompareTo(object o)
    {
        if (o is Upgrade other)
        {
            if (price < other.price) return -1;
            if (price == other.price) return 0;
        }
        return 1;
    }
}
