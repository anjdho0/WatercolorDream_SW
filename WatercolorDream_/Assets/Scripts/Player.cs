using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CMYK cmyk;
    public Camera scope;
    public Rigidbody rigidbody;
    public float speed;
    Quaternion dontturn;

	// Use this for initialization
	void Start () {
        cmyk = new CMYK(Color.white);
        scope = GetComponentInChildren<Camera>();
        rigidbody = GetComponent<Rigidbody>();
        speed = 0.3f;
        dontturn = transform.rotation;
        GameObject.Find("Main Camera").SetActive(false);
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
        transform.rotation = dontturn;
	}
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tile") && rigidbody.velocity.y <= 0) 
        {
            cmyk += CMYK.RGBToCMYK(collision.gameObject.GetComponentInChildren<MeshRenderer>().material.color);
            gameObject.GetComponent<MeshRenderer>().material.color = cmyk.CMYKToRGB();
            rigidbody.AddForce(collision.gameObject.transform.up * 300, ForceMode.Acceleration);
        }
    }
}
