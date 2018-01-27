using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {


    ////Define Mock-Physgraph-here
    //public PhysNode[] physNodeList = { new PhysNode(0,10), new PhysNode(1,0), new PhysNode(2,10,2), new PhysNode(3,0), new PhysNode(4,0) };

    //public int[,] connectionMatrix = {
    //    {0,1,0,0,1 },
    //    {1,0,1,1,0 },
    //    {0,1,0,0,0 },
    //    {0,1,0,0,0 },
    //    {1,0,0,0,0 } };

    //public int[,] peopleFlow =
    //{
    //    {0,2,5,0,2 },
    //    {0,0,0,0,0 },
    //    {0,0,0,4,6 },
    //    {0,0,0,0,0 },
    //    {0,0,0,0,0 },
    //};

    //Define Mock-Physgraph-here
    
    public static readonly float moduleDisplacement = 1.0f; //TODO: Move to Public Variables

    [SerializeField]
    PhysNode baseNode;

    //Starting maxID: 0 (Base)
    private int currentMaxGraphId = 0; //TODO: Move to Public Variables

    bool running = false;

    //public PhysNode[] physNodeList = {  };
    public List<PhysNode> physNodeList = new List<PhysNode>();

    public int[,] connectionMatrix = {
        {0} };

    public int[,] peopleFlow =
    {
        {0}
    };

    Runner runner;

    void Start()
    {
        runner = new Runner();
        physNodeList.Add(baseNode);
        setAllShortestPathArrayLength(physNodeList.Count);
    }

    private void Update()
    {
        if(running)
            runner.Running();
    }

    public void AddNode(PhysNode newNode)
    {
        //Extend connection Matrix
        int[,] oldConnMatr = connectionMatrix;
        int[,] newConnMatr = InsertNodeToMatrix(oldConnMatr, 1);
        connectionMatrix = newConnMatr;

        //Extend peopleFlow Matrix
        int[,] oldPeopleFlowMatr = peopleFlow;
        int[,] newPeopleFlowMatr = InsertNodeToMatrix(oldPeopleFlowMatr, 1);
        peopleFlow = newPeopleFlowMatr;


        //Add PhysNode
        physNodeList.Add(newNode);
        setAllShortestPathArrayLength(physNodeList.Count);
        
    }

    public void setAllShortestPathArrayLength(int amount)
    {
        foreach (PhysNode node in physNodeList)
        {
            node.setShortestPathArrLength(amount);
        }
    }

    /// <summary>
    /// Adds Rows and Columns to the Connection Matrix
    /// </summary>
    /// <param name="oldMatr">The old Connection Matrix</param>
    /// <param name="numElemToAdd"></param>
    /// <returns></returns>
    int[,] InsertNodeToMatrix(int[,] oldMatr, int numElemToAdd)
    {
        var newMatr = new int[oldMatr.GetLength(0) + numElemToAdd, oldMatr.GetLength(1) + numElemToAdd];
        for (var i = 0; i < newMatr.GetLength(0); ++i)
        {
            for (var j = 0; j < newMatr.GetLength(1); ++j)
            {
                if (i >= oldMatr.GetLength(0) || j >= oldMatr.GetLength(1))
                {
                    newMatr[i, j] = 0;
                    continue;
                }
                newMatr[i, j] = oldMatr[i, j];
            }
        }

        return newMatr;
    }
    


    /// <summary>
    /// Returns a new ID for a graph Node. The new ID is 1 higher than the currentMaxGraphId, which gets incremented every time this Method is called.
    /// </summary>
    /// <returns>A new graph node ID</returns>
    public int GetNewId()
    {
        return ++currentMaxGraphId;
    }


    /// <summary>
    /// Sets the Connection between the Nodes with ID1 and ID2 in the connection Matrix
    /// </summary>
    /// <param name="ID1">ID of the first Node</param>
    /// <param name="ID2">ID of the second Node</param>
    /// <returns></returns>
    public bool SetConnection(int ID1, int ID2)
    {
        if (ID1 >= connectionMatrix.Length || ID2 >= connectionMatrix.Length)
            return false;

        connectionMatrix[ID1, ID2] = connectionMatrix[ID2, ID1] = 1;

        return true;
    }


    public void Init()
    {
        for (int i = 0; i < physNodeList.Count; i++)
        {
            List<int> horizon = new List<int>();
            horizon.Clear();
            Path[] paths = new Path[physNodeList.Count];
            for(int j = 0; j < physNodeList.Count; j++)
            {
                paths[j] = new Path();
            }
            paths[i].lowestCost = 0;

            int cur = i;
            horizon.AddRange(AddToHorizon(cur, paths));
            while (horizon.Count>0)
            {
                cur = horizon[0];
                horizon.RemoveAt(0);
                horizon.AddRange(AddToHorizon(cur, paths));
            } 
            for(int j = 0; j < physNodeList.Count; j++)
            {
                if(paths[j].shortestPath.Count>0)
                physNodeList[i].shortestPath[j] = paths[j].shortestPath[0];
            }
            //Debug.Log(i + "-->");
            for(int j = 0; j < paths.Length; j++)
            {
                //Debug.Log(j);
                for (int x = 0; x < paths[j].shortestPath.Count; x++)
                {
                    //Debug.Log(paths[j].shortestPath[x]);
                }
                //Debug.Log("");
            }
        }
        setRandomPeopleFlow();
        Debug.Log("Graph Init Done");
    }

    private List<int> AddToHorizon(int cur, Path[] paths)
    {
        List<int> add = new List<int>();
        for(int i = 0; i < paths.Length; i++)
        {
            if (connectionMatrix[cur, i] > 0)
            {
                if (paths[i].lowestCost > (paths[cur].lowestCost + 1))
                {
                    paths[i].lowestCost = paths[cur].lowestCost + 1;
                    paths[i].shortestPath = new List<int>(paths[cur].shortestPath);
                    paths[i].shortestPath.Add(i);

                    add.Add(i);
                }
            }
        }

        return add;
    }

    /// <summary>
    /// Prints the connection Matrix
    /// </summary>
    public void PrintConnMatr()
    {

        PrintMatrix(connectionMatrix);

        //int[,] connMatr = connectionMatrix;

        //if (connMatr.GetLength(0) == 0 || connMatr.GetLength(1) == 0)
        //{
        //    Debug.Log("The connection Matrix is empty!");
        //    return;
        //}

        //string matrix = "";

        //for (int i = 0; i < connMatr.GetLength(1); i++)
        //{
        //    string output = "[" + connMatr[i, 0];

        //    for (int j = 1; j < connMatr.GetLength(0); j++)
        //    {
        //        output += ", " + connMatr[i, j];
        //    }

        //    output += "]";
        //    matrix += "\n" + output;
        //}

        //Debug.Log(matrix);
    }

    public void PrintMatrix(int[,] matrix)
    {

        if (matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
        {
            Debug.Log("The connection Matrix is empty!");
            return;
        }

        string matrixString = "";

        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            string output = "[" + matrix[i, 0];

            for (int j = 1; j < matrix.GetLength(0); j++)
            {
                output += ", " + matrix[i, j];
            }

            output += "]";
            matrixString += "\n" + output;
        }

        Debug.Log(matrixString);
    }


    public void runSim()
    {
        runner.Run(this);
        running = true;
    }

    public void setRandomPeopleFlow()
    {
        for (int i = 0; i < connectionMatrix.GetLength(1); i++)
        {
            for (int j = 0; j < connectionMatrix.GetLength(0); j++)
            {
                if(connectionMatrix[i, j] > 0)
                {
                    //Different peopleFlowfor each Direction
                    int flow = Random.Range(0, physNodeList[i].Inhab);
                    peopleFlow[i, j] = flow;
                }
            }
        }

        PrintMatrix(peopleFlow);

    }


}
