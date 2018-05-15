﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapSpawning : MonoBehaviour {

    public List<Map> maps = new List<Map>();
    public GameObject area, player;
    public Map curMap;
    public int stageNum;
    GameManager gameManager;
    

    // Use this for initialization
    void Start () {
        ImportingMap();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stageNum = gameManager.stageNum;
        curMap = maps[stageNum];
        MapSpawn();
        gameManager.fsm.UpdateStates();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
            NextStage();
	}

    void ImportingMap()
    {
        StreamReader file = new StreamReader("Assets\\Scripts\\MapList2.txt");
        bool isFile = file != null ? true : false;
        Debug.Log(isFile);
        string curLine = file.ReadLine();

        while (curLine != null)
        {
            Debug.Log("start importing");

            if (curLine.StartsWith("st"))
            {
                Debug.Log("stage");
                Map thisStage = new Map();
                int areaNum = 0;
                //List<int> areaPosArr = new List<int>();

                while (true)
                {
                    curLine = file.ReadLine();
                    if (curLine.StartsWith("end"))
                        break;
                    Debug.Log("area" + areaNum);
                    int i = 0;
                    string[] dataArr = curLine.Split(' ');
                    int[] areaArr = new int[7];
                    int nextPosition = -1;
                    Color[] colors = new Color[7];

                    foreach (var t in dataArr)
                    {
                        if (t[0] - '0' >= 0 && t[0] - '0' <= 3)
                        {
                            areaArr[i] = t[0] - '0';
                            if(t[0] - '0' != 0)
                            {
                                string[] colorData = t.Split(',', '(', ')');
                                colors[i].r = Convert.ToInt32(colorData[1]) / 255.0f;
                                colors[i].g = Convert.ToInt32(colorData[2]) / 255.0f;
                                colors[i].b = Convert.ToInt32(colorData[3]) / 255.0f;
                            }
                            i++;
                        }
                        else
                        {
                            nextPosition = t[0] - '4';
                        }
                    }
                    thisStage.AddArea(areaArr, nextPosition, colors);
                    areaNum++;
                }
                maps.Add(thisStage);
            }
            curLine = file.ReadLine();
        }

        file.Close();
    }

    void NextStage()
    {
        stageNum++;
        curMap = maps[stageNum];
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
