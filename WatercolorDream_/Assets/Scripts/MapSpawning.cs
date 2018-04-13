using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawning : MonoBehaviour {

    public List<Map> maps = new List<Map>();
    public GameObject area;
    public Map curMap;
	
    // Use this for initialization
	void Start () {
        maps.Add(new Map());
        TileTypes[] testArea1 = {TileTypes.normaltile, TileTypes.normaltile, TileTypes.normaltile,
                                TileTypes.normaltile, TileTypes.empty, TileTypes.normaltile, TileTypes.empty };
        TileTypes[] testArea2 = {TileTypes.empty, TileTypes.empty, TileTypes.normaltile,
                                TileTypes.normaltile, TileTypes.normaltile, TileTypes.normaltile, TileTypes.normaltile};
        maps[0].AddArea(testArea1);
        maps[0].AddArea(testArea2);
        curMap = maps[0];
        MapSpawn();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MapSpawn()
    {
        Vector3 areaSpawnPos = transform.position;

		for(int i = 0; curMap.map[i] != null; i++)
        {
            
            GameObject curArea = Instantiate(area, areaSpawnPos, transform.rotation, transform);
            curArea.GetComponent<Area>().Init(curMap.map[i]);

        }

    }
}
