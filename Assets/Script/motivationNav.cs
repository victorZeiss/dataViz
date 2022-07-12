using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motivationNav : MonoBehaviour
{


    public GameObject message;
    public Camera camera;
    public GameObject camerasSetup;
    public float time;
    public GameObject labelTrigger;

    public GameObject placeCamera;


    public Material activeMat;
    public Material unactiveMat;
    
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
            if ( Physics.Raycast (ray,out hit,100.0f)) {
                StartCoroutine(ScaleMe(hit.transform));
               // Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }
        
    }

 
    IEnumerator ScaleMe(Transform objTr) {

        if(string.Equals(objTr.transform.name, labelTrigger.name)){

            message.SetActive(false);
            labelTrigger.GetComponent<MeshRenderer>().material = unactiveMat;
            Vector3 startingPos  = camerasSetup.transform.position;
            Vector3 finalPos =  placeCamera.transform.position;
            float elapsedTime = 0;
            
            while (elapsedTime < time)
            {
                camerasSetup.transform.position= Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            message.SetActive(true);
            labelTrigger.GetComponent<MeshRenderer>().material = activeMat;
        }
 
        yield return new WaitForSeconds(0.1f);
       

        
    }
}
