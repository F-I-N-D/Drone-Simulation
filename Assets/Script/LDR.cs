using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDR : MonoBehaviour
{
    public RenderTexture ldr;
    public float lightLevel;
    public int light;

    // Update is called once per frame
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

        Debug.Log(lightLevel);
    }
}
