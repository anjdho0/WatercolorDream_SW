using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CMYK cmyk;
    public Camera scope;
    public Rigidbody rigidbody;
    float speed;
    float angle;
    Quaternion dontturn;
    bool isFalling;
    bool canMove;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        GameObject.Find("Main Camera").SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cmyk = new CMYK(Color.white);
        scope = GetComponentInChildren<Camera>();
        rigidbody = GetComponent<Rigidbody>();
        speed = 0.3f;
        angle = 2.0f;
        isFalling = true;
        canMove = true;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(gameObject.transform.up, (-1) * angle);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Rotate(gameObject.transform.up, angle);
        }
        if (rigidbody.velocity.y <= 0 && transform.position.y >= 5)
        {
            isFalling = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(gameObject.transform.forward.normalized * speed);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tile") && isFalling) 
        {
            isFalling = false;
            cmyk += CMYK.RGBToCMYK(collision.gameObject.GetComponentInChildren<MeshRenderer>().material.color);
            gameObject.GetComponent<MeshRenderer>().material.color = cmyk.CMYKToRGB();
            rigidbody.AddForce(collision.gameObject.transform.up * 300, ForceMode.Acceleration);
        }
    }

    private IEnumerator PlayerMovement()
    {
        yield return null;
    }
}
