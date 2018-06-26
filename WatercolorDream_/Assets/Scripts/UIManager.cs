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
    }

    public void OnClickedStageButton(GameObject button)
    {
        if (button.name.Equals("back"))
        {
            MainMenu.SetActive(true);
            SelectStage.SetActive(false);
            gameManager.fsm.next = StateType.MainMenu;
        }
        else
        {
            gameManager.stageNum = button.name[button.name.Length - 1] - '1';
            Debug.Log(gameManager.stageNum);
            gameManager.fsm.next = StateType.LoadStage;
        }
    }

    public void OnClickedInGameMenu()
    {
        gameManager.fsm.next = StateType.InGameMenu;
        InGameMenu.SetActive(true);
    }

    public void OnClickedResume()
    {
        gameManager.fsm.next = StateType.Resume;
        InGameMenu.SetActive(false);
    }

    public void OnClickedRetry()
    {
        gameManager.fsm.next = StateType.Retry;
    }

    public void OnClickedGiveUp()
    {
        gameManager.fsm.next = StateType.GiveUp;
    }
}
