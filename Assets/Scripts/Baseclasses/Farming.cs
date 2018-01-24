using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : PhysNode {

    //Before adding Start and Update, check if they would override Methods in PhysNode
    new void Start()
    {
        base.Start();
        
        Inhab = (int)(Random.Range(0.0f, 1.0f) * 15);
    }
}
