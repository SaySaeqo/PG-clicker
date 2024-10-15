using System;
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

    private GameState() {}

    public int studentCounterAvalange = 0;
    public int studentCounter = 0;

    public Dictionary<string, int> boughtLecturers = new Dictionary<string, int>();
    public int studentsPerSecond
    {
        get
        {
            int sum = 0;
            foreach ((string name, int power) in boughtLecturers)
            {
                double multiplayer;
                if (!lecturersMultipliers.TryGetValue(name, out multiplayer)) multiplayer = 1.0;
                
                sum += (int)(power * multiplayer);
            }
            return sum;
        }
    }
    public int socialMoney = 0;

    public double globalStudentMultiplier = 1;

    public Dictionary<string, double> lecturersMultipliers = new Dictionary<string, double>();


}
