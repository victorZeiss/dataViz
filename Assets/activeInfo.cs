using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeInfo : MonoBehaviour
{

    public GameObject message;
    public GameObject title;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void  OnCollisionEnter(Collision col) {


        Debug.Log(col.gameObject.name );
        if (col.gameObject.name == player.name){
            message.SetActive(true);
            title.SetActive(true);
        }
       
 

        
    }


    void OnCollisionExit (){
            message.SetActive(false);
            title.SetActive(false);
    }


}
