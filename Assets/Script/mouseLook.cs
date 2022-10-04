using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        setInitialAngle();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerBody.GetComponent<freeWalk>().activeWalk  && Input.GetMouseButton(0)  ){
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity *Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity *Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

        }
        
    }

    public void setInitialAngle(){
        xRotation = this.transform.rotation.eulerAngles.x;
    }
}
