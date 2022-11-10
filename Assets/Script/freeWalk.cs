using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeWalk : MonoBehaviour
{

    public bool activeWalk;
    public float speed = 20f;
    public GameObject cameraSet;
    public GameObject posInit;
    private Vector3 moveInput;

    Rigidbody rigidbody;



    void Start(){
        rigidbody = GetComponent<Rigidbody>();
    }
 
     void Update () {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = posInit.transform.position;
            
        }
 

     }


     void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward =  new Vector3(cameraSet.transform.forward.x, 0f, cameraSet.transform.forward.z);
        Vector3 cameraRight =  new Vector3(cameraSet.transform.right.x, 0f, cameraSet.transform.right.z);

        if(activeWalk){
            Vector3 movement = (cameraForward * z + cameraRight * x).normalized;
            rigidbody.velocity = movement * speed;
        }

    }


}
