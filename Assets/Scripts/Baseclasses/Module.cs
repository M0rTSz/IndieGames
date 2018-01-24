//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class PhysNode : MonoBehaviour {

//    public PhysNode physNode;

//    [SerializeField]
//    private Connector[] connections;


//    private int inhabitants;
//    private int infected;

//    public int Inhabitants
//    {
//        get
//        {
//            return inhabitants;
//        }

//        set
//        {
//            inhabitants = value;
//        }
//    }

//    public int Infected
//    {
//        get
//        {
//            return infected;
//        }

//        set
//        {
//            infected = value;
//        }
//    }

//    // Use this for initialization
//    void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {

//    }
    
//    /// <summary>
//    /// Returns the attached Connector in Direction connectorDirection
//    /// </summary>
//    /// <param name="connectorDirection">The Direction the desired Connector should face</param>
//    public Connector getConnectorByDirection(Vector3 connectorDirection)
//    {
//        for(int i = 0; i < connections.Length; i++)
//        {
//            connections[i].RecalculateDirection();
//            if (connections[i].Direction - connectorDirection == Vector3.zero)
//            {
//                return connections[i];
//            }
//        }
//        return null;
//    }

//    public void setupPhysNode(int id, int inhab, int ill)
//    {
//        physNode = new PhysNode(id, inhab, ill);
//    }
//}
