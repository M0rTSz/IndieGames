using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A Module to create Energy
public class Generator : PhysNode {

    private float productionRate;

    //Before adding Start and Update, check if they would override Methods in PhysNode
    new void Start()
    {
        base.Start();

        inhabitable = false;
        productionRate = 2.0f;

        Inhab = (int)(Random.Range(0.0f, 1.0f) * 15);
    }

    protected override void DoSim()
    {
        GameData.Energy += (int)(productionRate * Inhab); //Inhabs of Farming are working and therefor creating food
    }

}
