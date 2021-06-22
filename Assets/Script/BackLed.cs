using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLed : MonoBehaviour
{
    // Start is called before the first frame update
    public string colorBack;
    void Start()
    {
        //Set the color for game object using html value

        Color color;
        if (ColorUtility.TryParseHtmlString(colorBack, out color))
        {
            GetComponent<Renderer>().material.color = color;
        }


    }

 
}
