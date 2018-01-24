using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysNode : MonoBehaviour
{
    private readonly float infectedBarYOffset = 0.2f;
    private readonly float startingWaitingTime = 1.0f;

    public int id;

    public int[] shortestPath = { };

    public int level;

    [SerializeField]
    private Connector[] connections;

    [SerializeField]
    GameObject infectedBarObject;

    private GameObject canvas;

    [SerializeField]
    protected int inhab, ill;

    /// <summary>
    /// Updates InfectedBar when setting value
    /// </summary>
    public int Inhab
    {
        get
        {
            return inhab;
        }

        set
        {
            setInhab(value);
        }
    }

    /// <summary>
    /// Updates InfectedBar when setting value
    /// </summary>
    public int Ill
    {
        get
        {
            return ill;
        }

        set
        {
            setIll(value);
        }
    }

    //public PhysNode()
    //{
    //    this.id = -1;
    //    this.inhab = 0;
    //    this.ill = 0;
    //}

    //public PhysNode(int id, int inhab)
    //{
    //    this.id = id;
    //    this.inhab = inhab;
    //    this.ill = 0;
    //}

    //public PhysNode(int id, int inhab, int ill)
    //{
    //    this.id = id;
    //    this.inhab = inhab;
    //    this.ill = ill;
    //}

    protected void Start()
    {
        canvas = GameObject.FindWithTag("PhysicalCanvas");
        infectedBarObject = Instantiate(infectedBarObject);
        infectedBarObject.transform.SetParent(canvas.transform);
        infectedBarObject.transform.position = gameObject.transform.position + new Vector3(0, infectedBarYOffset, 0);
        infectedBarObject.transform.rotation = infectedBarObject.transform.parent.rotation;
        infectedBarObject.GetComponent<InfectedBar>().Fade(false, startingWaitingTime);


        //Debug.Log("Extend new Node:");
        //extendShortestPathArr(FindObjectOfType<Graph>().physNodeList.Count);
    }

    /// <summary>
    /// Returns the attached Connector in Direction connectorDirection
    /// </summary>
    /// <param name="connectorDirection">The Direction the desired Connector should face</param>
    public Connector getConnectorByDirection(Vector3 connectorDirection)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i].RecalculateDirection();
            if (connections[i].Direction - connectorDirection == Vector3.zero)
            {
                return connections[i];
            }
        }
        return null;
    }
    public void setupNode(int id, int inhab, int ill)
    {
        this.id = id;
        this.Inhab = inhab;
        this.Ill = ill;
    }

    private void setIll(int ill)
    {
        this.ill = ill;
        UpdateInfectedBarValue();
    }

    private void setInhab(int inhab)
    {
        this.inhab = inhab;
        UpdateInfectedBarValue();
    }

    public void addIll(int ill)
    {
        this.Ill += ill;
        UpdateInfectedBarValue();
    }

    public void addInhab(int inhab)
    {
        this.Inhab += inhab;
        UpdateInfectedBarValue();
    }

    public void UpdateInfectedBarValue()
    {
        infectedBarObject.GetComponent<InfectedBar>().setValue(Inhab > 0.0f ? ((float)Ill / (float)Inhab) : 1.0f);
    }

    //public void extendShortestPathArr(int amount)
    //{
    //    int[] newArr = new int[shortestPath.Length + amount];
        
    //    for(int i = 0; i < newArr.Length; i++)
    //    {
    //        newArr[i] = -1;
    //    }

    //    shortestPath = newArr;
    //    Debug.Log(shortestPath.Length);
    //}

    public void setShortestPathArrLength(int amount)
    {
        int[] newArr = new int[amount];

        for (int i = 0; i < amount; i++)
        {
            if (i < shortestPath.Length)
                newArr[i] = shortestPath[i];
            else
                newArr[i] = -1;
        }

        shortestPath = newArr;
    }

    public void toggleVisible(bool visible)
    {

    }

    private void OnMouseEnter()
    {
        infectedBarObject.GetComponent<InfectedBar>().Fade(true);
    }

    private void OnMouseExit()
    {
        infectedBarObject.GetComponent<InfectedBar>().Fade(false);
    }


}
