using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour{
    
    [SerializeField]
    MeshFilter meshFilter;

    [SerializeField]
    MeshRenderer meshRenderer;

    [SerializeField]
    MeshCollider meshCollider;

    Mesh mesh;

    
    private void Awake()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.DrawHexagon(gameObject.transform, 5.0f);
        
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
