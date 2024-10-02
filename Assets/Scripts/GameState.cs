using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState
{
    private static GameState instance;
    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public int studentCounterAvalange = 0;
    public int studentCounter = 0;
    public int studentsPerSecond = 0;
    public int socialMoney = 0;

    public double globalStudentMultiplier = 1;

    public Dictionary<string, double> lecturersMultipliers = new Dictionary<string, double>();


}
