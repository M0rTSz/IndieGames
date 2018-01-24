using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataHolder {

    private MainDataHolder instance = null;



    public MainDataHolder Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MainDataHolder();
            }
            return instance;
        }
    }

    //Dataholders per Scene can be created by extend this class!
    //Use Scenedataholders to Store everything which is needed by multiple Classes
}
