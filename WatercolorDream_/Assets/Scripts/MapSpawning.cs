using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapSpawning : MonoBehaviour {

    public List<Map> maps = new List<Map>();
    public GameObject area;
    public Map curMap;
    public int stageNum;

    // Use this for initialization
    void Start () {
        ImportingMap();
        stageNum = 0;
        curMap = maps[0];
        MapSpawn();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
            NextStage();
	}

    void ImportingMap()//st가 맵의 시작, end가 맵의 종료. MapList의 0~3의 정수는 타일의 타입. 줄의 마지막에 있는 4이상의 정수들은 다음 타일이 어디로 생성될 지를 정함
    {
        StreamReader file = new StreamReader("Assets\\Scripts\\MapList.txt");
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
                List<int> areaPosArr = new List<int>();

                while (true)
                {
                    curLine = file.ReadLine();
                    if (curLine.StartsWith("end"))
                        break;
                    Debug.Log("area" + areaNum);
                    int i = 0;
                    int[] areaArr = new int[7];
                    int nextPosition = -1;

                    foreach (var t in curLine)
                    {
                        if (t.CompareTo(' ') == 0)
                        {
                            continue;
                        }
                        else if (t - '3' > 0)
                        {
                            nextPosition = t - '4';
                        }
                        else
                        {
                            areaArr[i] = t - '0';
                            Debug.Log(areaArr[i]);
                            i++;
                        }
                    }
                    thisStage.AddArea(areaArr, nextPosition);
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

		for(int i = 0; i < curMap.map.Count; i++)
        {
            
            GameObject curArea = Instantiate(area, areaSpawnPos, transform.rotation, curStage.transform);
            curArea.GetComponent<Area>().Init(curMap.map[i]);
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
