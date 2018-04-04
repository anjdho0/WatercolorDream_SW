﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CMYK cmyk;
    public Camera scope;
    public Rigidbody rigidbody;
    public float speed;

	// Use this for initialization
	void Start () {
        cmyk = new CMYK(Color.white);
        scope = GetComponentInChildren<Camera>();
        rigidbody = GetComponent<Rigidbody>();
        speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(gameObject.transform.right.normalized * (-1) * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(gameObject.transform.right.normalized * speed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(gameObject.transform.forward.normalized * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(gameObject.transform.forward.normalized * (-1) * speed);
        }
	}
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            rigidbody.AddForce(collision.gameObject.transform.up * 300, ForceMode.Acceleration);
        }
    }
}
