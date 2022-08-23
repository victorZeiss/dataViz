using UnityEngine;

public class Wand : MonoBehaviour
{
    GameObject gc;
    GraphController gcScript;
    private LineRenderer beam;

    private Camera cam;

    private Vector3 origin;
    private Vector3 endPoint;
    private Vector3 mousePos;

    private Transform currentSelection;

    void Start()
    {
        // Grabbed our laser.
        beam = this.gameObject.AddComponent<LineRenderer>();
        beam.startWidth = 0.03f;
        beam.endWidth = 0.03f;

        // Grab the main camera.
        cam = Camera.main;

        gc = GameObject.Find("Graph Controller");
        gcScript = gc.GetComponent<GraphController>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            checkLaser();
        }
        else beam.enabled = false;
    }


    void checkLaser()
    {       
        origin = this.transform.position + this.transform.forward * 0.2f * this.transform.lossyScale.z;
       
        mousePos = Input.mousePosition;
        mousePos.z = 30f;
        endPoint = cam.ScreenToWorldPoint(mousePos);

       
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        if (currentSelection!= null && (currentSelection.tag == gcScript.graphTag)) 
        {
            currentSelection.GetComponent<Renderer>().material = gcScript.regularMat;
            currentSelection = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, 300f))
        {
            
            endPoint = hit.point;
            currentSelection = hit.collider.transform;
           
            if (hit.collider.tag == gcScript.graphTag)
            {
                gcScript.callInformationPanel();
                currentSelection.GetComponent<Renderer>().material = gcScript.highlightMat;
                //hit.transform.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);              
            }
   
        }
                
        beam.SetPosition(0, origin);
        beam.SetPosition(1, endPoint);
        beam.enabled = true;

    }


}