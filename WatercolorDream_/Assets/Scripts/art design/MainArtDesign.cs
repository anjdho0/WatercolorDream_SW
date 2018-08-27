using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainArtDesign : MonoBehaviour {

	DirtyLensFlare lensflare;

	Vector3 start;
	Vector3 end;
	float timer = 0.0f;
	float rotateTime = 10.0f;
	const float changeTime = 3.0f;

	class LensFlareSetting {
		public float threshold;
		public float saturation;
		public float intensity;

		public LensFlareSetting (float t, float s, float i) {
			this.threshold = t;
			this.saturation = s;
			this.intensity = i;
		}
	}

	LensFlareSetting main = new LensFlareSetting (0.2f,-1.2f,7.0f);
	LensFlareSetting select = new LensFlareSetting (0.6f, 1.2f, 3.0f);

	void Start() {

		lensflare = Camera.main.GetComponent<DirtyLensFlare>();
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

	public void ChangeLensFlare() {

		if (lensflare == null)
			return;

		StartCoroutine(_changeLensFlare());
	}

	IEnumerator _changeLensFlare() {

		for (float timer = 0.0f; timer < changeTime; timer += Time.deltaTime) {
			lensflare.threshold = Mathf.Lerp(main.threshold, select.threshold, timer / changeTime);
			lensflare.saturation = Mathf.Lerp(main.saturation, select.saturation, timer / changeTime);
			lensflare.flareIntensity = Mathf.Lerp(main.intensity, select.intensity, timer / changeTime);

			rotateTime = Mathf.Lerp(10.0f, 30.0f, timer / changeTime);

			yield return null;
		}
		
	}

}
