using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringRotation : MonoBehaviour
{

    public GameObject cameraSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 cameraForward =  new Vector3(cameraSet.transform.forward.x, 0f, cameraSet.transform.forward.z);
       Vector3 nordGlobal = Vector3.forward;

       float angle =   Mathf.Sign(cameraSet.transform.forward.x) * Mathf.Acos((Vector3.Dot(cameraForward,nordGlobal)) / (cameraForward.magnitude * nordGlobal.magnitude));

       transform.localRotation = Quaternion.Euler(0f, radToDeg(angle), 0f);


    
    
    }

    public float radToDeg(float radians)
    {
        float degrees = -(180 / Mathf.PI) * radians;
        return (degrees);
    }
}
