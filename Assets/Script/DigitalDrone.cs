using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalDrone : MonoBehaviour
{
    RaycastHit hit;
    
    public float laserDown = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position,  -Vector3.up, out hit))
        {
            float groundDistance = hit.distance + 0.05f;
            Debug.DrawRay(transform.position, -transform.up, Color.green);
          //Debug.Log(groundDistance);
        }
    }
}
