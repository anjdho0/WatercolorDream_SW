using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Area : MonoBehaviour {

    public GameObject[] tiles;
    public TileTypes[] tilesTypes;
    public Color[] colors;
    
	void Awake () {
        tiles = new GameObject[7];
        colors = new Color[7];
        int i = 0;
        foreach(var tile in GetComponentsInChildren<MeshRenderer>())
        {
            tiles[i] = tile.gameObject;
            colors[i] = tile.material.color;
            i++;
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
                tiles[i].layer = LayerMask.NameToLayer("DestTile");
                tiles[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
