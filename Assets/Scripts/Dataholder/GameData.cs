using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static GameObject baseModule;

    private static int energy = 100;
    private static int food = 100;

    //TODO: Somehow keep track of the entire Population
    //Caution: Currently the Inhabitants of the Farming Module are handeled as workers and therefor counted also in their home Module
    public static int population;

    public static float gameInterval = 3.0f;

    public static int Food
    {
        get
        {
            return food;
        }

        set
        {
            food = value;
            if(food < 0)
            {
                Debug.Log("No food left."); //Do something
                food = 0;
            }
        }
    }

    public static int Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            if (energy < 0)
            {
                Debug.Log("No energy left."); //Do something
                energy = 0;
            }
        }
    }
}

public enum Modules { Base, Farming, EnergyGenerator, Habitation }