using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();
    public TileTypes[] tilesTypes;

	// Use this for initialization
	void Start () {
        foreach(var tile in GetComponentsInChildren<MeshRenderer>())
        {
            tiles.Add(tile.gameObject);
        }
        tiles[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
        tiles[1].GetComponent<MeshRenderer>().material.color = Color.black;
        tiles[2].GetComponent<MeshRenderer>().material.color = Color.cyan;
        tiles[3].GetComponent<MeshRenderer>().material.color = Color.blue;
        tiles[4].GetComponent<MeshRenderer>().material.color = Color.grey;
        tiles[5].GetComponent<MeshRenderer>().material.color = Color.red;
        tiles[6].GetComponent<MeshRenderer>().material.color = Color.green;

    }

    public void Init(TileTypes[] area)
    {
        tilesTypes = area;
        GameObject[] tiles_ = tiles.ToArray();

        for(int i = 0; i < 7; i++)
        {
            if (area[i] == TileTypes.empty)
                tiles_[i].SetActive(false);
                //Destroy(tiles[i]);
            if (area[i] == TileTypes.starttile)
                tiles[i].GetComponent<MeshRenderer>().material.color = Color.white;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
