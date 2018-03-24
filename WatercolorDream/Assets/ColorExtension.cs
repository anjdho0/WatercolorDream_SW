using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension {

	public static Color RGBToCMYK(this Color rgb)
    {
        float k = 1 - Mathf.Max(rgb.r, rgb.g, rgb.b);
        float c = (1 - rgb.r - k) / (1 - k);
        float m = (1 - rgb.g - k) / (1 - k);
        float y = (1 - rgb.b - k) / (1 - k);
        return new Color(c, m, y);
    }
    
}
