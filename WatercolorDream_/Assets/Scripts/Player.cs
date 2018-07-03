using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public CMYK cmyk;
    public Rigidbody rigidbody;
    float speed;
    float angle;
    Quaternion dontturn;
    bool isFalling;
    bool canMove;
    GameManager gameManager;
    Touch touch;
    GameObject scope;
    float dist;

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

	}

    // Update is called once per frame
    void FixedUpdate() { //공중에 있는시간 : 2.459998 앞으로 가는거리 : 18.44998
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

        if (rigidbody.velocity.y <= 0 && transform.position.y >= 5)
        {
            isFalling = true;
        }

        transform.Translate((transform.forward.normalized) * speed, Space.World);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tile") && isFalling) 
        {
            isFalling = false;
            cmyk += CMYK.RGBToCMYK(collision.gameObject.GetComponent<MeshRenderer>().material.color);
            GetComponent<MeshRenderer>().material.color = cmyk.CMYKToRGB();
            rigidbody.AddForce(collision.gameObject.transform.up * 600, ForceMode.Acceleration);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("DestTile"))
        {
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
            DrawingScope((transform.forward.normalized * 18.44998f) + hit.point);
        }
        else
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.up * (-1), out hit);
            scope.transform.position = hit.point + (transform.forward.normalized * 18.44998f);
        }
    }

    void DrawingScope(Vector3 pos)
    {
        scope = new GameObject("Scope");
        scope.transform.position = pos;
        Mesh mesh = new Mesh();
        MeshRenderer meshRenderer = scope.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = scope.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        meshRenderer.material = Resources.Load<Material>("Glass");
        Vector3[] vertices = new Vector3[361];
        Vector2[] uvs = new Vector2[361];
        int[] triangles = new int[360 * 3];
        vertices[0] = new Vector3(0, 0, 0);
        uvs[0] = new Vector2(0, 0);
        float angle = Mathf.PI / 180;

        for (int i = 1; i < 361; i++)
        {
            float x = Mathf.Cos(angle * (i - 1));
            float y = Mathf.Sin(angle * (i - 1));
            vertices[i] = new Vector3(x, 0, y);
            uvs[i] = new Vector2(x, y);
        }

        int index = 1;
        for(int i = 0; i < triangles.Length; i += 3)
        {
            triangles[i] = 0;
            triangles[i + 1] = index;
            triangles[i + 2] = index != 360 ? index + 1 : 1;
            index++;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    bool CalculateScore(CMYK tile, CMYK player)
    {
        float score = 0.1f;
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
        return true;
        //Debug.Log("Fail");
        // return false;
    }

}
