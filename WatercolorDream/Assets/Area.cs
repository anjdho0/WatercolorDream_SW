using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //[SerializeField]
    //[SerializeField]

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh mesh;
    float radius = 5.0f;
    
    private void Awake()
    {
        Vector3[] vertecties = new Vector3[7];
        int[] tris = new int[18];
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = mesh;
        vertecties[0] = new Vector3(0, 0);

        for(int i = 1; i < 7; i++)
        {
            vertecties[i] = new Vector3(radius * Mathf.Sin(Mathf.PI / 3 * i), radius * Mathf.Cos(Mathf.PI / 3 * i));
        }
        mesh.vertices = vertecties;

        for(int i = 0; i <6; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            if (i == 5)
            {
                tris[i * 3 + 2] = 1;
            }
            else
            {
                tris[i * 3 + 2] = i + 2;
            }

        }
        mesh.triangles = tris;
    }

}
