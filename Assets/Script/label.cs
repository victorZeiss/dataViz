using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Label{
    [SerializeField] Material inactiveMat;
    [SerializeField] Material activeMat;
    [SerializeField] GameObject labelTrigger;
    [SerializeField] bool selected;


    public Material  InactiveMat{
        get{ return inactiveMat;}
    }

    public Material  ActiveMat{
        get{ return activeMat;}
    }

     public GameObject LabelTrigger{
        get{ return labelTrigger;}
    }

      public bool Selected{
        get{ return selected;}
        set{ selected = value;}
    }


    

}
