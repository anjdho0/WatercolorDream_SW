using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public FSM fsm = new FSM();
    public List<Map> stages = new List<Map>();
    public int stageNum = 0;

	void Awake () {
        ImportingMap();
        fsm.current = StateType.MainMenu;
        fsm.next = StateType.MainMenu;
        Debug.Log("manager start");
	}
	
	void Update () {
        fsm.UpdateStates();
	}

    void ImportingMap()
    {
        stages.Clear();
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
                if (curLine.EndsWith("C"))
                {
                    thisStage.isCleared = true;
                }

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
                            if (t[0] - '0' != 0)
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
                stages.Add(thisStage);
            }
            curLine = file.ReadLine();
        }

        file.Close();
    }

}
