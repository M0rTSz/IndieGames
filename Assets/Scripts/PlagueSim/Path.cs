using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

    public int lowestCost;
    public List<int> shortestPath;

    public Path()
    {
        lowestCost = int.MaxValue;
        shortestPath = new List<int>();
    }

}
