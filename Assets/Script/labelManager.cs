using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class labelManager : MonoBehaviour
{
    public GameObject [] motivations;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        animCall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void animCall(){

        int index = 0;
        for(int i = 0; i <  motivations.Length; i++){
            if(motivations[i].GetComponent<motivationNav>().label.Selected){
                index++;
            }
            
        }
        if(index <  motivations.Length )
         motivations[index].GetComponent<motivationNav>().label.LabelTrigger.GetComponent<Animator>().SetTrigger("anim");

      
    }
}
