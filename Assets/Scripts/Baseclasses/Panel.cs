using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

    Connector myConnector;

    PanelManager pm;

    public Connector MyConnector
    {
        get
        {
            return myConnector;
        }

        set
        {
            myConnector = value;
        }
    }

    private void Start()
    {
        pm = FindObjectOfType<PanelManager>();
    }

    void OnMouseExit()
    {
        pm.RemoveCurrentPanel();
    }

    public void AddModule(int moduleType)
    {
        myConnector.AddModule((Modules) moduleType);
        pm.RemoveCurrentPanel();
    }

    public void RemovePanel()
    {
        pm.RemoveCurrentPanel();
    }
    
}