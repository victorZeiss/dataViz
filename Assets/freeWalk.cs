using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeWalk : MonoBehaviour
{

    public bool activeWalk;
    public float speed = 20f;

    public GameObject cameraSet;
 
     void Update () {
         Vector3 pos = transform.position;
         Vector3 cameraForward =  new Vector3(cameraSet.transform.forward.x, 0f, cameraSet.transform.forward.z);
         Vector3 cameraRight =  new Vector3(cameraSet.transform.right.x, 0f, cameraSet.transform.right.z);

            if(activeWalk){
    
            if (Input.GetKey ("w")) {
                pos += cameraForward * speed * Time.deltaTime;
            }
            if (Input.GetKey ("s")) {
                pos -= cameraForward *speed * Time.deltaTime;
            }

            if (Input.GetKey ("a")) {
                pos -= cameraRight * speed * Time.deltaTime;
            }
            if (Input.GetKey ("d")) {
                pos += cameraRight * speed * Time.deltaTime;
            }

           
             
        }
         transform.position = pos;
     }
}
