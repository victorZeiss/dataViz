using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Linq;

public class GraphController : MonoBehaviour
{
    public string inputFile;
    public Material highlightMat;
    public Material regularMat;
    public GameObject barPrefab;
    public GameObject labelPrefab;
    [HideInInspector] public JSONData data;
    [HideInInspector] public string graphTag = "Bar";

    //Set scale bars
    float platformScale;
    public float graphBorder = 2;
    public float barSize = 0.1f;
    public float barInterval = 0.2f;

    public float barAreaSide = 0.5f;


    
    Vector2 mouseScroll;

    // Start is called before the first frame update
    void Start()
    {
        string jsonPath = Application.streamingAssetsPath + "/TestJson.json";
        string jsonStr = File.ReadAllText(jsonPath);
        data= JsonUtility.FromJson<JSONData>(jsonStr);

        //GameObject platform = GameObject.Find("Platform");

        //this.transform.parent = platform.transform;

        //platformScale = platform.transform.localScale.x;
        
        buildBarGraph(data);

    }

    // Update is called once per frame
    void Update()
    {
        mouseScroll = Input.mouseScrollDelta;
        if (mouseScroll.y != 0){
            //scaleGraph(mouseScroll.y);
        }
       
        
    }

    void buildBarGraph(JSONData data) 
    {
        //Debug.Log(data.dataItems[0].Name);
        float maxGraphLength = (platformScale - graphBorder*2);
        Vector3 barStartPosition = this.transform.position;

        List<float> sizeArray = new List<float>();
        foreach (DataItem d in data.dataItems) 
        {
            if (d.Size > 0) {
                sizeArray.Add(d.Size);
            }
                
        }

        int barCount = sizeArray.Count();
        float minVlaue = sizeArray.Min();
        float maxValue = sizeArray.Max();

        float graphHLength = (barSize * barCount) + (barInterval * (barCount - 1));

        /*if ((maxGraphLength / graphHLength) % 2 == 0)
        {
             barStartPosition = maxGraphLength / graphHLength;
        }
        else 
        {
             barStartPosition = (maxGraphLength / graphHLength)+0.5f;
        }*/

        int i = 0;
        foreach (DataItem d in data.dataItems) 
        {
            
            if (d.Size > 0) 
            {

                //GameObject bar = Instantiate(barPrefab, new Vector3(-barStartPosition, 0.5f, 0), Quaternion.identity);
                GameObject bar = Instantiate(barPrefab, barStartPosition, Quaternion.identity);
                bar.transform.parent = this.transform;
                //bar.tag = graphTag;

                regularMat = bar.GetComponent<Renderer>().material;
                

                float normalisedValue = (0.9f*(d.Size - minVlaue) / (maxValue - minVlaue)) + 0.1f;
                //float scaledValue = Mathf.Log10(d.Size);
                bar.transform.localScale = new Vector3(barSize, normalisedValue, barSize);
                bar.transform.position = barStartPosition - i*barInterval* Vector3.right;
                bar.AddComponent<Rigidbody>();

                GameObject barLabel = Instantiate(labelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                barLabel.AddComponent<RotateLabel>();
                TextMesh textComponent1 = barLabel.GetComponent<TextMesh>();
                textComponent1.text = d.Name;
                textComponent1.characterSize = 0.5f;
                barLabel.transform.parent = bar.transform;
                //barLabel.transform.position = new Vector3(barStartPosition-0.05f, 0.1f, -0.1f);
                //barLabel.transform.position = barStartPosition;

                GameObject barValue = Instantiate(labelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                barValue.AddComponent<RotateLabel>();
                TextMesh textComponent2 = barValue.GetComponent<TextMesh>();
                textComponent2.text = d.Size.ToString()+"MB";
                textComponent2.characterSize = 0.5f;
                barValue.transform.parent = bar.transform;
                //barValue.transform.position = new Vector3(barStartPosition-0.05f, normalisedValue+0.15f, -0.1f);
                //barValue.transform.position = barStartPosition;
                i++;

                //barStartPosition -= barInterval;
            }
            
           
        }

    }

    /*void scaleGraph(float mouseScroll) 
    {
        Vector3 currentSize = this.transform.localScale;
        this.transform.localScale = new Vector3(currentSize.x + (mouseScroll/1000), currentSize.y + (mouseScroll/10), currentSize.z + (mouseScroll/1000));
        this.transform.position = new Vector3(0,0,0);
    }*/

    public void callInformationPanel() 
    {

        Debug.Log("Killin it");
    }
}
