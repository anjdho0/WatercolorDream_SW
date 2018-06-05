using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject MainMenu, SelectStage, InGameMenu;

    GameManager gameManager;

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickedSelectStageButton()
    {
        Debug.Log("SelectStageclicked");
        MainMenu.SetActive(false);
        SelectStage.SetActive(true);
        gameManager.fsm.next = StateType.SelectStage;
        gameManager.fsm.UpdateStates();
    }

    public void OnClickedStageButton(GameObject button)
    {
        if (button.name.Equals("back"))
        {
            MainMenu.SetActive(true);
            SelectStage.SetActive(false);
            gameManager.fsm.next = StateType.MainMenu;
            gameManager.fsm.UpdateStates();
        }
        else
        {
            gameManager.stageNum = button.name[button.name.Length - 1] - '1';
            Debug.Log(gameManager.stageNum);
            gameManager.fsm.next = StateType.LoadStage;
            gameManager.fsm.UpdateStates();
        }
    }

    public void OnClickedInGameMenu()
    {
        InGameMenu.SetActive(true);
    }
}
