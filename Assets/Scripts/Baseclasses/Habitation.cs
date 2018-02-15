using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A Module used to create Food
public class Habitation : PhysNode {
        
    //Before adding Start and Update, check if they would override Methods in PhysNode
    new void Start()
    {
        base.Start();

        inhabitable = true;
        
        Inhab = (int)(Random.Range(0.0f, 1.0f) * maxInhab);

        graph.refreshPeopleFlow(this);
    }

    protected override void DoSim()
    {
        GameData.Food -= Inhab;
    }
}
