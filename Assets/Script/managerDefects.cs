using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerDefects : MonoBehaviour
{
    public GameObject [] defectsInfo;
    public int indexDefect = 0 ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void toolUpdate(){
        if(indexDefect <  defectsInfo.Length){
           defectsActive(indexDefect);
        }

    }

    public void defectsActive(int index){

        for(int i = 0; i < defectsInfo.Length; i++){
            if(i <= index){
                defectsInfo[i].SetActive(true);
            }else{
                defectsInfo[i].SetActive(false);
            }
        }
    }


    
   
}
