using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CMYK cmyk;
    public GameObject scopeSprite;
    GameObject scope;
    Rigidbody rigidbody;
    float speed;
    float angle;
    bool isFalling;
    bool canMove;
    bool isStarted;
    GameManager gameManager;
    Touch touch;

	// Use this for initialization
	void Start () {
        GameObject.Find("Main Camera").SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cmyk = new CMYK(Color.white);
        rigidbody = GetComponent<Rigidbody>();
        speed = 0.15f;
        angle = 2.0f;
        isFalling = true;
        canMove = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        isStarted = false;

	}

    // Update is called once per frame
    void FixedUpdate()
    { //공중에 있는시간 : 2.459998 앞으로 가는거리 : 18.44998
        if (gameManager.fsm.current != StateType.InGame && canMove)
        {
            canMove = false;
            Debug.Log("CannotMove");
        }
        else if (gameManager.fsm.current == StateType.InGame && !canMove)
        {
            canMove = true;
            Debug.Log("CanMove");
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || touch.position.x < 0)
            {
                scope.transform.SetParent(transform);
                transform.Rotate(transform.up, (-1) * angle);
                scope.transform.SetParent(null);
            }
            if (Input.GetKey(KeyCode.RightArrow) || touch.position.x > 0)
            {
                scope.transform.SetParent(transform);
                transform.Rotate(transform.up, angle);
                scope.transform.SetParent(null);
            }
            if (transform.position.y <= -10 && gameManager.fsm.current != StateType.GameOver)
            {
                gameManager.fsm.next = StateType.GameOver;
            }
        }

        if (cmyk.CMYKToRGB() == Color.black)
        {
            gameManager.fsm.next = StateType.GameOver;
        }

        if (rigidbody.velocity.y <= 0 && transform.position.y >= 5)
        {
            isFalling = true;
        }

        if (isStarted)
        {
            transform.Translate((transform.forward.normalized) * speed, Space.World);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Tile") || collision.gameObject.layer == LayerMask.NameToLayer("StartTile")) && isFalling) 
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("StartTile"))
            {
                isStarted = true;
            }

            isFalling = false;
            cmyk += CMYK.RGBToCMYK(collision.gameObject.GetComponent<MeshRenderer>().material.color);
            GetComponent<MeshRenderer>().material.color = cmyk.CMYKToRGB();
            rigidbody.AddForce(collision.gameObject.transform.up * 600, ForceMode.Acceleration);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("DestTile"))
        {
            Debug.Log("finished");
            Time.timeScale = 0;
            if(CalculateScore(CMYK.RGBToCMYK(collision.gameObject.GetComponent<MeshRenderer>().material.color), cmyk))
            {
                gameManager.fsm.next = StateType.Clear;
            }
            else
            {
                gameManager.fsm.next = StateType.Result;
            }
        }

        if(scope == null)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.up * (-1), out hit);
            DrawingScope((transform.forward.normalized * 18.44998f) + hit.point + new Vector3(0, 0.001f, 0));
        }
        else
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.up * (-1), out hit);
            scope.transform.position = hit.point + (transform.forward.normalized * 18.44998f) + new Vector3(0, 0.001f, 0);
        }
    }

    void DrawingScope(Vector3 pos)
    {
        scope = Instantiate(scopeSprite);
        scope.name = "scope";
        scope.transform.position = pos;
        scope.transform.Rotate(new Vector3(1, 0, 0), 90);
    }

    bool CalculateScore(CMYK tile, CMYK player)
    {
        Vector3 v1 = new Vector3(tile.c, tile.m, tile.y);
        Vector3 v2 = new Vector3(player.c, player.m, player.y);
        v2 = v2 / v2.magnitude;
        float score = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        Debug.Log(score);
        gameManager.score = score;
        if (score > 0.99f)
        {
            Debug.Log("Complete");
            return true;
        }
        else if (score > 0.7f && score < 0.99f)
        {
            Debug.Log("Clear");
            return true;
        }

        Debug.Log("Fail");
        return false;
    }

    Color GetColorUnderScope()
    {
        Color color;
        RaycastHit hit;
        Physics.Raycast(scope.transform.position, new Vector3(0, 1, 0), out hit);
        GameObject hitted = hit.collider.gameObject;
        if(hitted.layer == LayerMask.NameToLayer("Tile"))
        {
            color = hitted.GetComponent<MeshRenderer>().material.color;
            Debug.Log("getcolor");
            return color;
        }
        else
        {
            return cmyk.CMYKToRGB();
        }

    }

}
