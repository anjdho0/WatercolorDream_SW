using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    List<GameObject> Tiles = new List<GameObject>();
	// Use this for initialization
	void Start () {
        GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
