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
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       
            foreach (GameObject drone in ServerConn.drones)
            {
                Vector3 screenPos =  cam.WorldToScreenPoint(drone.transform.position);
              // Debug.Log("drone" + drone.GetComponent<HardwareDrone>().id + ":    " + (int) screenPos.x + " pixels from the left");
            //Debug.Log("drone" + drone.GetComponent<HardwareDrone>().id + ":    " + (int) screenPos.y + " pixels from the above");
        }
        
    }

    void SoftDronePos()
    {
       
    }

   
}
