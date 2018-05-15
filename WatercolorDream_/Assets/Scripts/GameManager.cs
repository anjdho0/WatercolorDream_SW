using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public FSM fsm = new FSM();
    Dictionary<int, bool> playerInfo = new Dictionary<int, bool>();
    public int stageNum = 0;

	void Start () {
        fsm.current = StateType.MainMenu;
        fsm.next = StateType.SelectStage;
        Debug.Log("manager start");
	}
	
	void Update () {
       
	}
}
