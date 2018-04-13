using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMYK {

    public float c;
    public float m;
    public float y;
    public float k;

    public CMYK(float c, float m, float y, float k)
    {
        this.c = c;
        this.m = m;
        this.y = y;
        this.k = k;

    }

    public CMYK(Color rgb)
    {
        float k = 1 - Mathf.Max(rgb.r, rgb.g, rgb.b);
        float c = (1 - rgb.r - k) / (1 - k);
        float m = (1 - rgb.g - k) / (1 - k);
        float y = (1 - rgb.b - k) / (1 - k);
    }

    public Color CMYKToRGB()
    {
        Color rgb = new Color(0, 0, 0);
        rgb.r = (1 - c) * (1 - k);
        rgb.g = (1 - m) * (1 - k);
        rgb.b = (1 - y) * (1 - k);
        return rgb;
    }
	
}
