using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeWalk : MonoBehaviour
{

    public bool activeWalk;
    public float speed = 20f;
    public float limitBorder = 9f;
    public Vector3 objectClose;

    public GameObject cameraSet;


    
    private Rigidbody _Rigidbody;
    private Vector3 moveInput;
 
     void Update () {
        

     }


     void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward =  new Vector3(cameraSet.transform.forward.x, 0f, cameraSet.transform.forward.z);
        Vector3 cameraRight =  new Vector3(cameraSet.transform.right.x, 0f, cameraSet.transform.right.z);

        if(activeWalk){
            this.transform.position += z * cameraForward * Time.deltaTime * speed;
            this.transform.position += x * cameraRight * Time.deltaTime * speed;
        }

    }





}
