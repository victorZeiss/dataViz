using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToProblems : MonoBehaviour
{
    public Camera camera;
    public string nameScene;
    public GameObject placeCamera;
    public GameObject camerasSetup;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
      // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0) && camera != null){ 
            RaycastHit hit; 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);          
           
            if ( Physics.Raycast (ray,out hit,Mathf.Infinity)) {
                Debug.Log(hit.transform.name);
                StartCoroutine(beginProblems(hit.transform));
               
            }
        }
        
    }



    IEnumerator beginProblems(Transform objTr) {

        if(string.Equals(objTr.transform.name, this.name)){
            
            camerasSetup.transform.parent = null;

            Vector3 startingPos  = camerasSetup.transform.position;
            Vector3 finalPos =  placeCamera.transform.position;
            Quaternion startingRot = camerasSetup.transform.rotation;
            Quaternion finalRot = placeCamera.transform.rotation;


            float elapsedTime = 0;
            
            
            while (elapsedTime < time)
            {
                camerasSetup.transform.position= Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                camerasSetup.transform.rotation= Quaternion.Lerp(startingRot, finalRot, (elapsedTime / time));
                elapsedTime += Time.deltaTime;               
                yield return null;
            }


            SceneManager.LoadScene(nameScene);


        }

 
        yield return new WaitForSeconds(0.1f);
       

        
    }

}
