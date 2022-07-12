using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navWalkIntro : MonoBehaviour
{


    public GameObject introMessage;
    public Camera camera;
    public GameObject camerasSetup;
    public float time;

    public GameObject placeCamera;
    public GameObject toolInit;


    public bool tutorialMode;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0) && camera != null){ 
            RaycastHit hit; 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
           
            if ( Physics.Raycast (ray,out hit,Mathf.Infinity)) {
                 Debug.Log(hit.transform.name);
                if(tutorialMode)
                StartCoroutine(beginnWalk(hit.transform));
               // Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }
        
    }

 
    IEnumerator beginnWalk(Transform objTr) {

        if(string.Equals(objTr.transform.name, toolInit.name)){

            Vector3 startingPos  = camerasSetup.transform.position;
            Vector3 finalPos =  placeCamera.transform.position;

            Quaternion startingRot =  camerasSetup.transform.rotation;
            Quaternion endingRot =  placeCamera.transform.rotation;

            float elapsedTime = 0;
            
            while (elapsedTime < time)
            {
                camerasSetup.transform.position= Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                camerasSetup.transform.rotation = Quaternion.Lerp(startingRot, endingRot, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }


            yield return new WaitForSeconds(0.5f);
            introMessage.SetActive(true);
            camerasSetup.transform.SetParent(placeCamera.transform);
            placeCamera.GetComponent<freeWalk>().activeWalk = true;
            tutorialMode = false;

            
        }
 
        yield return new WaitForSeconds(0.1f);
       

        
    }
}

