using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The Base Module. There should only be one Base
public class Base : PhysNode
{
    new void Start()
    {
        base.Start();

        if(GameData.baseModule == null)
        {
            GameData.baseModule = this.gameObject;
        } else
        {
            Destroy(GameData.baseModule);
            GameData.baseModule = this.gameObject;
        }

        inhabitable = true;
        graph.refreshPeopleFlow(this);
    }

    //Before adding Start and Update, check if they would override Methods in PhysNode
    protected override void DoSim()
    {
        Debug.Log("In base");
        GameData.Food -= this.Inhab;
    }
}
