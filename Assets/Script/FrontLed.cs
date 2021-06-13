using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class FrontLed : MonoBehaviour
{
    // Start is called before the first frame update
    public string colorFront;
    void Start()
    {
        //Create a new cube primitive to set the color on
        

        //Get the Renderer component from the new cube
        
        Color color;
        if(ColorUtility.TryParseHtmlString(colorFront, out color))
        {
            GetComponent<Renderer>().material.color = color;
        }
        
        
        }

        //Call SetColor using the shader property name "_Color" and setting the color to red

    // Update is called once per frame
    void Update()
    {
        
    }
}
