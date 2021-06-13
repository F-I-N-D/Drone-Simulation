using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Server;
public class PixelPosition : MonoBehaviour
{
    //public Transform drone;
    Camera cam;
    //List<GameObject> drones;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
        //ShowSecondDisplay();
        //cam = GetComponent<Camera>();
        //cam.aspect = 16.0f / 9.0f;
    }

    // Update is called once per frame
    void Update()
    {
       
            foreach (GameObject drone in ServerConn.hardwareDrones)
            {
             //   Vector3 screenPos =  cam.WorldToScreenPoint(drone.transform.position);
            //Debug.Log("drone" + drone.GetComponent<DigitalDrone>().id + ":    " + (int)(screenPos.x) + " pixels from the left");
            //Debug.Log("drone" + drone.GetComponent<DigitalDrone>().id + ":    " + (int)(screenPos.y) + " pixels from the above");
        }

    }

    void SoftDronePos()
    {
       
    }

   
}
