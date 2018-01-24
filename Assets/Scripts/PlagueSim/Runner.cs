using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner
{

    public int simInterval = 3;
    private float elapsedTime = 0;
    public Graph g;
    public int[][] watch;
    
    public TextMesh[] textObject = new TextMesh[5];

    // Use this for initialization
    void Start()
    {
        //Run();
    }

    // Update is called once per frame
    public void Running() //Old Update
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > simInterval)
        {
            doSim();
            elapsedTime = 0;
            for (int i = 0; i < g.physNodeList.Count; i++)
            {
                writeTo(i);
            }
        }

    }

    void Run()
    {
        g = new Graph();
        g.Init();
        initPop();
        for (int i = 0; i < g.physNodeList.Count; i++)
        {
            writeTo(i);
        }
    }

    public void Run(Graph newGraph)
    {
        g = newGraph;
        newGraph.Init();
        initPop();
        for (int i = 0; i < newGraph.physNodeList.Count; i++)
        {
            writeTo(i);
        }
    }

    void initPop()
    {
        for (int i = 0; i < g.physNodeList.Count; i++)
        {
            for (int j = 0; j < g.physNodeList.Count; j++)
            {
                if (g.peopleFlow[i, j] > 0)
                {
                    traversGraphInit(i, j);
                }
            }
        }
    }

    void doSim()
    {
        for (int i = 0; i < g.physNodeList.Count; i++)
        {
            simStep(i);
        }
    }

    void simStep(int id)
    {
        for (int i = 0; i < g.physNodeList.Count; i++)
        {
            if (g.peopleFlow[id, i] > 0)
            {
                traversGraphInfect(id, i);
            }
        }
    }

    void traversGraphInfect(int from, int to)
    {
        int cur = from;
        while (cur != to)
        {
            cur = g.physNodeList[cur].shortestPath[to]; //Moved up
            infect(from, cur, 0.5f); //Changed from 0.05 to 0.5
        }
    }

    void traversGraphInit(int from, int to)
    {
        int cur = from;
        while (cur != to)
        {
            cur = g.physNodeList[cur].shortestPath[to];
            if (cur < 0)
            {
                break;
            }
            addInhab(cur, g.peopleFlow[from, to]);
        }
    }

    void addInhab(int id, int amt)
    {
        g.physNodeList[id].Inhab += amt;
    }

    void infect(int from, int cur, float perc)
    {
        if (g.physNodeList[cur].Inhab > g.physNodeList[cur].Ill)
        {
            float rnd = Random.Range(0.0f, 1.0f);
            if (rnd < (g.physNodeList[from].Ill * perc)) //Changed to Range(float, float);
            {
                //g.physNodeList[cur].Ill++;
                int newInfected = Mathf.Clamp(Mathf.RoundToInt(g.peopleFlow[from, cur] * rnd), 0, g.physNodeList[from].Inhab);
                g.physNodeList[cur].Ill += newInfected;
            }
        }
    }

    void writeTo(int id)
    {
        string text = "";
        text += id + "\n";
        text += "Inhab: " + g.physNodeList[id].Inhab + "\n";
        text += "Ill: " + g.physNodeList[id].Ill + "\n";
        Debug.Log(text);
        //textObject[id].text = text;
    }
}
