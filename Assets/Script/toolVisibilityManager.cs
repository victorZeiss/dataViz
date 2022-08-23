using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolVisibilityManager : MonoBehaviour
{

    public GameObject [] tools;
    public GameObject [] toolsRings;
    public GameObject [] toolsArrow;

    public bool includeDefects;
    public GameObject [] defects;

    public int toolIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        toolActive(toolIndex );
        toolRingActive(toolIndex );
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toolUpdate(){
        if(toolIndex <  tools.Length){
            toolActive(toolIndex);
            toolRingActive(toolIndex);
            if (includeDefects)
                defectsActive(toolIndex);
        }

    }


    public void toolActive(int index){

        for(int i = 0; i < tools.Length; i++){
            if(i <= index){
                tools[i].SetActive(true);
            }else{
                tools[i].SetActive(false);
            }
        }
    }



    public void defectsActive(int index){

            for(int i = 0; i < defects.Length; i++){
                if(i == index){
                    defects[i].GetComponent<Animator>().SetTrigger("anim");
                }else{
                    defects[i].GetComponent<Animator>().SetTrigger("cancel");
                }
            }
    }


    
    public void toolRingActive(int index){

        for(int i = 0; i < toolsRings.Length; i++){
            if(i == index){
                toolsRings[i].SetActive(true);
                toolsArrow[i].SetActive(true);
                toolsArrow[i].GetComponent<Animation>().Play("indexTool");

            }else{
                toolsRings[i].SetActive(false);
                toolsArrow[i].SetActive(false);
            }
        }
    }


    
}
