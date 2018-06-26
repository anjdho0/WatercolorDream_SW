using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    // Use this for initialization
    bool isEnd;
    Image screen;

	void Start () {
        isEnd = false;
        screen = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeOutStart()
    {
        StartCoroutine(Fadeout());
    }

    private IEnumerator Fadeout()
    {
        while(screen.color.a >= 1.0f)
        {
            Color color = screen.color;
            color.a += 0.05f;
            screen.color = color;
            yield return null;
        }
        yield return null;
    }
}
