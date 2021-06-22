using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDR : MonoBehaviour
{
    public RenderTexture ldr;
    public float lightLevel;

    // script for ldr using the camera view that's set on the ldr object.
    // the camera looks at the pixels that can be seen on the ldr object
    // it stores the the redered texture of the ldr object that's been made before any light signs on it
    // than it checks how much white pixel it sees
    // the more the camera see the white pixels the higher the light intensity is
    void Update()
    {
        RenderTexture tmpLDR = RenderTexture.GetTemporary(ldr.width, ldr.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(ldr, tmpLDR);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmpLDR;

        Texture2D temp2DTexture = new Texture2D(ldr.width, ldr.height);
        temp2DTexture.ReadPixels(new Rect(0, 0, tmpLDR.width, tmpLDR.height), 0, 0);
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmpLDR);

        Color32[] colors = temp2DTexture.GetPixels32();
        lightLevel = 0;
        for(int i = 0; i < colors.Length; i++)
        {
            lightLevel += (0.2126f*colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b);
        }
        lightLevel -= 5800000;
        Debug.Log(lightLevel);
    }
}
