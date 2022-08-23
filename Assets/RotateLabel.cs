using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLabel : MonoBehaviour
{
    GameObject user;
    Camera userCamera;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("WebXRCameraSet");
        userCamera = user.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(userCamera.transform);
        transform.rotation = Quaternion.LookRotation(userCamera.transform.forward);
    }
}
