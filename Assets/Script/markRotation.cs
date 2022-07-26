using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markRotation : MonoBehaviour
{
    public GameObject cameraSet;

    public GameObject modelTool;

    public GameObject toolMark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
       Vector3 lookAtTool =  modelTool.transform.position - cameraSet.transform.position;
       Vector3 lookOverPlane = new Vector3(lookAtTool.x, 0f, lookAtTool.z);
       Vector3 cameraForward =  new Vector3(cameraSet.transform.forward.x, 0f, cameraSet.transform.forward.z);        
       float angle =  - direction(cameraForward,lookOverPlane) * Mathf.Acos((Vector3.Dot(cameraForward,lookOverPlane)) / (cameraForward.magnitude *lookOverPlane.magnitude));
       float angleDeg = radToDeg(angle);
       Debug.Log(angleDeg);
  
       if(angleDeg < 33 && angleDeg > -33){
         transform.localRotation = Quaternion.Euler(0f, radToDeg(angle), 0f);
       }
      
    
    
    }

    public float radToDeg(float radians)
    {
        float degrees = -(180 / Mathf.PI) * radians;
        return (degrees);
       
    }

 

    public float direction(Vector3 forward, Vector3 point){
        Vector3 cross = Vector3.Cross(forward,point);
        return Mathf.Sign(cross.y);
        

    }

   
}
