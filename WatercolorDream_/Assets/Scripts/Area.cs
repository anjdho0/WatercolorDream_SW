using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();
    public TileTypes[] tilesTypes;
    public Color[] colors;

	// Use this for initialization
	void Awake () {
        foreach(var tile in GetComponentsInChildren<MeshRenderer>())
        {
            tiles.Add(tile.gameObject);
        }

    }

    public void Init(TileTypes[] area, Color[] _colors)
    {
        tilesTypes = area;
        colors = _colors;

        for(int i = 0; i < 7; i++)
        {
            if (area[i] == TileTypes.empty)
                Destroy(tiles[i]);

            if (area[i] == TileTypes.starttile)
                tiles[i].GetComponent<MeshRenderer>().material.color = Color.white;

            if (area[i] == TileTypes.normaltile)
                tiles[i].GetComponent<MeshRenderer>().material.color = colors[i];

            if (area[i] == TileTypes.desttile)
                tiles[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
