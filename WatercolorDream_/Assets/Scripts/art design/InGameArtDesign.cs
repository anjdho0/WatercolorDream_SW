using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameArtDesign : MonoBehaviour {

	float timer = 0.0f;
	float rotateTime = 300.0f;

	Vector3 start;
	Vector3 end;

	// Use this for initialization
	void Start () {
		start = new Vector3 (60.0f, 60.0f, 0.0f);
		end = start + new Vector3(0.0f, -360.0f, 360.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < rotateTime) {
			timer += 0.1f;
			transform.rotation = Quaternion.Euler(Vector3.Lerp(start, end, timer / rotateTime));
		}
		else {
			timer = 0.0f;
		}
	}
}
