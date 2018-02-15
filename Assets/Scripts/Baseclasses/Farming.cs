using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A Module used to create Food
public class Farming : PhysNode {

    private float productionRate;
    
    //Before adding Start and Update, check if they would override Methods in PhysNode
    new void Start()
    {
        base.Start();

        inhabitable = false;
        productionRate = 2.0f;
        
        Inhab = 0;
        graph.refreshPeopleFlow(this);
    }

    protected override void DoSim()
    {
        GameData.Food += (int)(productionRate * Inhab); //Inhabs of Farming are working and therefor creating food
    }
}
