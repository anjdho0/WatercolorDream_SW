using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainArtDesign : MonoBehaviour {

	Vector3 start;
	Vector3 end;
	float timer = 0.0f;
	const float rotateTime = 10.0f;

	void Start() {
		start = new Vector3(80.0f, 80.0f, 80.0f);
		end = start + new Vector3(360.0f, 360.0f, -360.0f);
	}

	// Update is called once per frame
	void Update () {

		if (timer < rotateTime) {

			timer += Time.deltaTime;
			Camera.main.transform.rotation = Quaternion.Euler (Vector3.Lerp(start, end, timer / rotateTime));

		}
		else {
			timer = 0.0f;
		}

	}
}
