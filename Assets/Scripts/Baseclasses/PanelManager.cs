using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    [SerializeField]
    public GameObject[] modules; //TODO: Move to Public Variables

    [SerializeField]
    GameObject canvas;


    private Camera c;

    private static GameObject currentPanel;


    private void Start()
    {
        c = Camera.main;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            RemoveCurrentPanel();
        }

	}

    public void ChangeCurrentPanel(GameObject Panel)
    {
        Destroy(currentPanel);
        currentPanel = Panel;
    }

    public void RemoveCurrentPanel()
    {
        Destroy(currentPanel);
        currentPanel = null;
    }

    /// <summary>
    /// Open an "Add a new Module" Panel
    /// </summary>
    /// <param name="connector">The Connector opening the Panel</param>
    /// <param name="panel">The corresponding Panel to open. Set Content in Connector</param>
    public void OpenPanelFromConnector(Connector connector, GameObject panel)
    {
        if(connector.Neighbor == null) //Connector is not occupied -> Module can be created
        {
            GameObject instance = Instantiate(panel);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = c.WorldToScreenPoint(connector.transform.position);
            instance.GetComponent<Panel>().MyConnector = connector;
            ChangeCurrentPanel(instance);
        } else //Connection between 2 existing Modules -> Modify Connection (cut etc...)
        {
            //TODO
        }
    }


}