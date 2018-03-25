using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtension {

	public static float[] RGBToCMYK(this Color rgb)
    {
        float[] cmyk = new float[4];
        float k = 1 - Mathf.Max(rgb.r, rgb.g, rgb.b);
        float c = (1 - rgb.r - k) / (1 - k);
        float m = (1 - rgb.g - k) / (1 - k);
        float y = (1 - rgb.b - k) / (1 - k);
        cmyk[0] = c;
        cmyk[1] = m;
        cmyk[2] = y;
        cmyk[3] = k;
        return cmyk;
    }

    public static Color CMYKToRGB(this float[] cmyk) 
    {
        Color rgb = new Color(0, 0, 0);
        rgb.r = (1 - cmyk[0]) * (1 - cmyk[3]);
        rgb.g = (1 - cmyk[1]) * (1 - cmyk[3]);
        rgb.b = (1 - cmyk[2]) * (1 - cmyk[3]);
        return rgb;
    }

    public static void DrawHexagon(this Mesh mesh, Transform transform, float radius)
    {
        Vector3[] vertecties = new Vector3[7];
        int[] tris = new int[18];
        vertecties[0] = transform.InverseTransformPoint(transform.position);

        for (int i = 1; i < 7; i++)
        {
            Vector3 d = new Vector3(radius * Mathf.Sin(Mathf.PI / 3 * i), + radius * Mathf.Cos(Mathf.PI / 3 * i));
            vertecties[i] = d;
        }
        mesh.vertices = vertecties;

        for (int i = 0; i < 6; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            if (i == 5)
            {
                tris[i * 3 + 2] = 1;
            }
            else
            {
                tris[i * 3 + 2] = i + 2;
            }

        }
        mesh.triangles = tris;
    }
    
}
