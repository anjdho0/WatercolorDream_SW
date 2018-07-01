using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapSpawning : MonoBehaviour {

    //public List<Map> maps = new List<Map>();
    public GameObject area, player;
    public Map curMap;
    public int stageNum;
    GameManager gameManager;
    

    // Use this for initialization
    void Start () {
        //ImportingMap();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stageNum = gameManager.stageNum;
        curMap = gameManager.stages[stageNum];
        MapSpawn();
        gameManager.fsm.UpdateStates();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
            NextStage();
	}

    void NextStage()
    {
        stageNum++;
        curMap = gameManager.stages[stageNum];
        Destroy(GameObject.Find("curStage"));
        MapSpawn();

    }

    public void MapSpawn()
    {
        GameObject curStage = new GameObject("curStage");
        Vector3 areaSpawnPos = transform.position;
        Instantiate(player,transform.position + new Vector3(0, 5, 0), transform.rotation);

		for(int i = 0; i < curMap.map.Count; i++)
        {
            
            GameObject curArea = Instantiate(area, areaSpawnPos, transform.rotation, curStage.transform);
            curArea.GetComponent<Area>().Init(curMap.map[i], curMap.areaColors[i]);
            if (curMap.areasPos[i] == NextAreaPos.left)
                areaSpawnPos += new Vector3(27, 0, 5.19615f);
            if (curMap.areasPos[i] == NextAreaPos.leftdown)
                areaSpawnPos += new Vector3(9, 0, 25.98076f);
            if (curMap.areasPos[i] == NextAreaPos.rightdown)
                areaSpawnPos += new Vector3(-9, 0, 25.98076f);
            if (curMap.areasPos[i] == NextAreaPos.right)
                areaSpawnPos += new Vector3(-27, 0, 5.19615f);
        }

    }
}
