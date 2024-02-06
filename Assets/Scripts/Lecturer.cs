using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Clicker/New lecturer")]
public class Lecturer : ScriptableObject
{
    [Header("General")]
    public Sprite face;
    public string lecturerName;
    public int power;
    public int price;
}
