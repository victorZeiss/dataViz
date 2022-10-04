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

    public  Color [] colorBars; 



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

    public float maxHeightBar = 0;
    float maxValue =0;
    float minValue = 0;


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
        minValue = sizeArray.Min();
        maxValue = sizeArray.Max();
        maxHeightBar = 10f* barSizeGrid;

        widthPlatform = (barCount) + (barCount-1 )*(barSpace);
        Vector3 barStartPosition = this.transform.position + Mathf.Round(widthPlatform/2) *(barSizeGrid)* Vector3.forward;

        int i = 0;
    
        foreach (DataItem d in data.dataItems) 
        {
            
            if (d.Size > 0) 
            {

                GameObject bar = Instantiate(barPrefab, barStartPosition, Quaternion.identity);
                bar.transform.parent = this.transform;

                //Bar position
                Vector3 positionBar = barStartPosition - i*(barSpace+1)*barSizeGrid* Vector3.forward;

                //Bar cube container
                GameObject containerCube = bar.transform.GetChild(0).gameObject;
                GameObject cube = containerCube.transform.GetChild(0).gameObject;

                regularMat = cube.GetComponent<Renderer>().material;

                float scaleFactor = (maxHeightBar * Mathf.Abs( (d.Size - minValue)) / (maxValue - minValue));
                containerCube.transform.localScale = new Vector3(barSizeGrid, scaleFactor, barSizeGrid);
                containerCube.transform.position = positionBar;
                cube.GetComponent<Renderer>().material.SetColor("_ColorTexture", colorBars[i]);
                cube.AddComponent<Rigidbody>();


                //Bar Label
                GameObject barLabel = Instantiate(labelPrefab, positionBar + Vector3.right* barSizeGrid * 2.5f + Vector3.up* 2f, Quaternion.identity);
                barLabel.AddComponent<RotateLabel>();
                TextMeshPro textComponent1 = barLabel.GetComponent<TextMeshPro>();
                textComponent1.text = d.Name;
                barLabel.transform.parent = bar.transform;
                barLabel.transform.localScale =  Vector3.one;
            
                //Bar Value
                GameObject barValue = Instantiate(labelPrefab, positionBar + Vector3.up* (scaleFactor + 5f) , Quaternion.identity);
                barValue.AddComponent<RotateLabel>();
                TextMeshPro textComponent2 = barValue.GetComponent<TextMeshPro>();
                textComponent2.text = d.Size.ToString()+"MB";
                barValue.transform.parent = bar.transform;
                barValue.transform.localScale = Vector3.one;

                i++;
            }
   
           
        }


    //Setup grid and platform
    setupPlatform();
    setupGrid(maxValue - minValue);

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


    public void setupGrid(float dist){

        //Check scaling factor graph max value in MB
        float distRound = Mathf.Ceil(dist);
        int numLines = gridSetup(distRound).Item1;
        int labelScale = gridSetup(distRound).Item2;

        float lineSpacing = maxHeightBar / numLines;

        
        for(int i= 1; i < numLines ; i++ ){
            //bar width
            GameObject lineGraphWidth = Instantiate(linePrefab, this.transform.position + new Vector3(-(heightPlatform+2)* barSizeGrid/2, i * lineSpacing, (widthPlatform+4) * barSizeGrid/ 2 ), Quaternion.identity);
            lineGraphWidth.transform.localScale = new Vector3(1f, 1f, (widthPlatform +2 )*barSizeGrid/2);
            // bar height
            GameObject lineGraphHeight = Instantiate(linePrefab, this.transform.position + new Vector3((heightPlatform+2)* barSizeGrid/2, i *lineSpacing, -(widthPlatform) * barSizeGrid/ 2 ), Quaternion.identity);
            lineGraphHeight.transform.localScale = new Vector3(1f, 1f, (heightPlatform +2 )*barSizeGrid/2);
            lineGraphHeight.transform.localRotation = Quaternion.Euler(0, 90f, 0f);

            //bar numbers
            float offsetZ = barSizeGrid * 0.5f;
            GameObject barValue = Instantiate(labelPrefab, this.transform.position + new Vector3(-(heightPlatform+2)* barSizeGrid/2, i * lineSpacing, ((widthPlatform+4) * barSizeGrid/ 2 )+ offsetZ  ) , Quaternion.identity);
            barValue.AddComponent<RotateLabel>();
            TextMeshPro textComponent = barValue.GetComponent<TextMeshPro>();
            textComponent.text = (i*(labelScale*10)).ToString();
            barValue.transform.localScale = Vector3.one * 0.7f;
        } 

        // label Units 

        float offsetZLabel = barSizeGrid * 2f ;
        GameObject labelY = Instantiate(labelPrefab, this.transform.position + new Vector3(-(heightPlatform+2)* barSizeGrid/2, maxHeightBar/2, ((widthPlatform+4) * barSizeGrid/ 2 )+ offsetZLabel  ) , Quaternion.identity);
        labelY.AddComponent<RotateLabel>();
        TextMeshPro textComponentLabelY= labelY.GetComponent<TextMeshPro>();
        textComponentLabelY.text = "Data(GB)";
        textComponentLabelY.fontStyle = FontStyles.Bold;
        labelY.transform.localScale = Vector3.one * 1.2f; 

        // label Units    

        GameObject labelX = Instantiate(labelPrefab, this.transform.position + new Vector3((heightPlatform+4)* barSizeGrid/2  + offsetZLabel , 3f, 0f  ) , Quaternion.identity);
        labelX.AddComponent<RotateLabel>();
        TextMeshPro textComponentLabelX= labelX.GetComponent<TextMeshPro>();
        textComponentLabelX.text = "Formats";
        textComponentLabelX.fontStyle = FontStyles.Bold;
        labelX.transform.localScale = Vector3.one * 1.2f; 

    }



    public Tuple<int, int> gridSetup(float maxValueData){
        // Get value in GB
        int numLines = (int)Mathf.Round(maxValueData) / 1000;
        int numZeros = 0;
        while(numLines > 100){
            numLines = numLines / 10;
            numZeros ++;
        }
        return Tuple.Create(numLines,numZeros);  
    }

    
}
