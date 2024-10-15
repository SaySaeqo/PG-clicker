using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Clicker/New lecturer")]
public class Lecturer : ScriptableObject, IComparable
{
    [Header("General")]
    public Sprite face;
    public string lecturerName;
    public int power;
    public int price;

    public int CompareTo(object o)
    {
        if (o is Lecturer other)
        {
            if (price < other.price) return -1;
            if (price == other.price) return 0;
        }
        return 1;
    }
}
