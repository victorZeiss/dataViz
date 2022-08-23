using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONData
{
    public DataItem[] dataItems;

}


[System.Serializable]
public class DataItem {

        public string Name;
        public float Size;
        public int Files;
        public string Type;

}
   
