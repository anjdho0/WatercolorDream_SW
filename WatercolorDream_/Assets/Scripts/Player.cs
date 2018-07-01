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
    Touch touch;

	// Use this for initialization
	void Start () {
        GameObject.Find("Main Camera").SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cmyk = new CMYK(Color.white);
        scope = GetComponentInChildren<Camera>();
        rigidbody = GetComponent<Rigidbody>();
        speed = 0.15f;
        angle = 2.0f;
        isFalling = true;
        canMove = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

	}

    // Update is called once per frame
    void Update() {
        if(gameManager.fsm.current != StateType.InGame && canMove)
        {
            canMove = false;
            Debug.Log("CannotMove");
        }
        else if(gameManager.fsm.current == StateType.InGame && !canMove)
        {
            canMove = true;
            Debug.Log("CanMove");
        }
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || touch.position.x < 0)
            {
                gameObject.transform.Rotate(gameObject.transform.up, (-1) * angle);
            }
            if (Input.GetKey(KeyCode.RightArrow) || touch.position.x > 0)
            {
                gameObject.transform.Rotate(gameObject.transform.up, angle);
            }
            if (gameObject.transform.position.y <= -10 && gameManager.fsm.current != StateType.GameOver)
            {
                gameManager.fsm.next = StateType.GameOver;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.Translate((gameObject.transform.forward.normalized) * speed, Space.World);
            }
        }
        if (rigidbody.velocity.y <= 0 && transform.position.y >= 5)
        {
            isFalling = true;
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tile") && isFalling) 
        {
            isFalling = false;
            cmyk += CMYK.RGBToCMYK(collision.gameObject.GetComponentInChildren<MeshRenderer>().material.color);
            gameObject.GetComponent<MeshRenderer>().material.color = cmyk.CMYKToRGB();
            rigidbody.AddForce(collision.gameObject.transform.up * 600, ForceMode.Acceleration);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("DestTile"))
        {
            gameManager.fsm.next = StateType.Clear;
        }
    }

   
}
