using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPosition : MonoBehaviour
{
    public Transform drone;
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(drone.position);
        Debug.Log( screenPos.x );
    }
}
