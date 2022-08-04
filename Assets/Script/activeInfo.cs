using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeInfo : MonoBehaviour
{

    public GameObject message;
    public GameObject title;
    public GameObject player;
    public GameObject compassUI;

    public GameObject compassManager;
    public int goTo;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void  OnTriggerEnter(Collider col) {
        //Debug.Log(col.gameObject.name );
        if (col.gameObject.name == player.name){
            message.SetActive(true);
            title.SetActive(true);
            compassUI.GetComponent<Animation>().Play("minCompass");
            compassManager.GetComponent<toolVisibilityManager>().toolIndex = goTo;
            compassManager.GetComponent<toolVisibilityManager>().toolUpdate();
        }
       
    }


    void OnTriggerExit (Collider col){
         if (col.gameObject.name == player.name){
            message.SetActive(false);
            title.SetActive(false);
            compassUI.GetComponent<Animation>().Play("maxCompass");
        }
            
    }

}
