using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    
    [SerializeField]
    private GameObject AddModulePanel;
    
    Graph graph;    
    PanelManager pm;

    [SerializeField]
    Connector neighbor;

    Vector3 direction;

    public Connector Neighbor
    {
        get
        {
            return neighbor;
        }

        set
        {
            neighbor = value;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    // Use this for initialization
    void Start () {
        direction = (transform.position - transform.parent.position).normalized;
        graph = FindObjectOfType<Graph>();
        pm = FindObjectOfType<PanelManager>();
    }

    void OnMouseDown()
    {
        //TODO: Build different Panels via Script
        pm.OpenPanelFromConnector(this, AddModulePanel);
    }
    
    /// <summary>
    /// Add a new Module next to the Module this Connector is attached to with 0 inhabitants and 0 infected people
    /// </summary>
    /// <param name="moduleType">The desired Module Type to create</param>
    public void AddModule(Modules moduleType)
    {
        GameObject newModule;

        //Insert Node
        //switch (moduleType)
        //{
        //    case Modules.Base:
        //        newModule = Instantiate(pm.modules[(int)Modules.Base], transform.parent.position + direction, Quaternion.identity);
        //        break;

        //    case Modules.Farming:
        //        newModule = Instantiate(pm.modules[(int)Modules.Farming], transform.parent.position + direction, Quaternion.identity);
        //        break;

        //    case Modules.EnergyGenerator:
        //        newModule = Instantiate(pm.modules[(int)Modules.EnergyGenerator], transform.parent.position + direction, Quaternion.identity);
        //        break;

        //    default:
        //        newModule = null;
        //        break;

        //}

        newModule = Instantiate(pm.modules[(int)moduleType], transform.parent.position + direction, Quaternion.identity);

        PhysNode newPhysNode = newModule.GetComponent<PhysNode>();
        if(newPhysNode.energyCreatingCost > GameData.Energy)
        {
            //Not enough Energy; somehow tell the Player via UI
            Debug.Log("Not enough Energy to create the Module");
            return;
        } else
        {
            GameData.Energy -= newPhysNode.energyCreatingCost;
        }

        //Insert Graph Node
        int newID = graph.GetNewId();
        int myID = this.transform.parent.GetComponent<PhysNode>().id;

        newPhysNode.setupNode(newID, 0, 0);

        graph.AddNode(newPhysNode);
        
        //Set the Connection between the 2 involved Connectors
        //TODO: Set Connections to other adjacent Modules (maybe via graph?)

        //Set Graph Connection
        graph.SetConnection(newID, myID);

        try
        {
            neighbor = newModule.GetComponent<PhysNode>().getConnectorByDirection(-direction);
            neighbor.Neighbor = this;

            //Set Level and Bottom Connector
            if (direction.y >= 1.0f)
            {
                //This is a connector at the Top
                neighbor.gameObject.SetActive(true);
                newPhysNode.level = transform.parent.gameObject.GetComponent<PhysNode>().level + 1; //The new Node is one level higher -> increase level
            } else {
                newPhysNode.level = transform.parent.gameObject.GetComponent<PhysNode>().level; //Same level as this module
            }

        } catch (System.NullReferenceException e)
        {
            Debug.Log("Connection konnte nicht gesetzt werden." + e.StackTrace);
        }

        graph.PrintConnMatr();

    }

    public void RecalculateDirection()
    {
        direction = (transform.position - transform.parent.position).normalized;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Connector"))
        {
            Connector otherCon = other.GetComponent<Connector>();
            otherCon.Neighbor = this;
            Neighbor = otherCon;
        }
    }

}