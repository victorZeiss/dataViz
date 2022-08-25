using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class GraphController : MonoBehaviour
{
    public string inputFile;
    public Material highlightMat;
    public Material regularMat;
    public GameObject barPrefab;
    public GameObject labelPrefab;
    public GameObject linePrefab;



    [HideInInspector] public JSONData data;
    [HideInInspector] public string graphTag = "Bar";

    //Set scale bars
    public float graphBorder = 2;
    public int barSizeGrid = 5;
    public int barSpace = 2;


    //Colelct dimension board
    public GameObject platform;
    public GameObject platformBorder;
    public GameObject gridPattern;

    public float widthPlatform;
    public float heightPlatform = 1f;


    // Start is called before the first frame update
    void Start()
    {
        string jsonPath = Application.streamingAssetsPath + "/TestJson.json";
        string jsonStr = File.ReadAllText(jsonPath);
        data= JsonUtility.FromJson<JSONData>(jsonStr);        
        buildBarGraph(data);

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void buildBarGraph(JSONData data) 
    {
  
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

        widthPlatform = (barCount) + (barCount-1 )*(barSpace);
        Vector3 barStartPosition = this.transform.position + Mathf.Round(widthPlatform/2) *(barSizeGrid)* Vector3.forward;


        //float graphHLength = (barSizeGrid * barCount) + (barInterval * (barCount - 1));



        int i = 0;
        foreach (DataItem d in data.dataItems) 
        {
            
            if (d.Size > 0) 
            {

                GameObject bar = Instantiate(barPrefab, barStartPosition, Quaternion.identity);
                bar.transform.parent = this.transform;

                Vector3 positionBar = barStartPosition - i*(barSpace+1)*barSizeGrid* Vector3.forward;
                regularMat = bar.transform.GetChild(0).GetComponent<Renderer>().material;
                float scaleFactor = (10f* barSizeGrid* Mathf.Abs( (d.Size - minVlaue)) / (maxValue - minVlaue));

                //float scaledValue = Mathf.Log10(d.Size);
                bar.transform.localScale = new Vector3(barSizeGrid, scaleFactor, barSizeGrid);
                bar.transform.position = positionBar;
                bar.AddComponent<Rigidbody>();


                float refactorTerm =  1f/ scaleFactor;

                if( scaleFactor < 1f){
                    refactorTerm = 0.3f;
                }
                //Bar Label
                GameObject barLabel = Instantiate(labelPrefab, positionBar + Vector3.right* 20f + Vector3.up* 2f, Quaternion.identity);
                barLabel.AddComponent<RotateLabel>();
                TextMeshPro textComponent1 = barLabel.GetComponent<TextMeshPro>();
                textComponent1.text = d.Name;
                //textComponent1.characterSize = 0.5f;
                barLabel.transform.parent = bar.transform;
                barLabel.transform.localScale = new Vector3(1f/barSizeGrid , refactorTerm, 1f/barSizeGrid );
                

                
      

                //Bar Value
                GameObject barValue = Instantiate(labelPrefab, positionBar + Vector3.up* (scaleFactor + 5f) , Quaternion.identity);
                barValue.AddComponent<RotateLabel>();
                TextMeshPro textComponent2 = barValue.GetComponent<TextMeshPro>();
                textComponent2.text = d.Size.ToString()+"MB";
                //textComponent2.characterSize = 0.5f;
                barValue.transform.parent = bar.transform;
                barValue.transform.localScale = new Vector3(1f/barSizeGrid , refactorTerm, 1f/barSizeGrid );
                


                i++;
            }


           
            
           
        }


    //Setup grid and platform
    setupPlatform();
    setupGrid(maxValue);
    


    }


    public void callInformationPanel() 
    {

        Debug.Log("Killin it");
    }


    public void setupPlatform() 
    {

        //platform blueSquares
        platform.transform.position = this.transform.position + Vector3.up * 0.01f + barSizeGrid* Vector3.forward;
        platform.transform.localScale =  new Vector3(heightPlatform +2, 1f, widthPlatform + 2);
        gridPattern.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2 ( heightPlatform+2, widthPlatform+2);
        
        //platform border
        platformBorder.transform.position = this.transform.position + Vector3.up * 0.01f + barSizeGrid* Vector3.forward;
        platformBorder.transform.localScale =  new Vector3(heightPlatform +2.5f, 1f, widthPlatform + 2.5f);
        
    }


    public void setupGrid(float maxValue){

        int numBars = (int)Mathf.Round(maxValue) / 10000;

        Debug.Log(numBars);

        //bar width
        for(int i= 1; i < numBars ; i++ ){
            GameObject lineGraphHeight = Instantiate(linePrefab, this.transform.position + new Vector3(-(heightPlatform+2)* barSizeGrid/2, i * 5f, (widthPlatform+4) * barSizeGrid/ 2 ), Quaternion.identity);
            lineGraphHeight.transform.localScale = new Vector3(1f, 1f, (widthPlatform +2) * barSizeGrid);

        }
            


    
    }
}
