using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject MainMenu, SelectStage, InGameMenu, Finish;

    GameManager gameManager;

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.fsm.current == StateType.Result)
        {
            ScoreUpdate();
        }
	}

    void ScoreUpdate()
    {
        Finish.transform.Find("Result/Text").GetComponent<Text>().text = "Result\n" + Mathf.Round(gameManager.score * 100).ToString() + "%";
    }

    public void OnClickedSelectStageButton(GameObject button)
    {
        if (button.transform.parent.name.Equals("Result"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        Debug.Log("SelectStageclicked");
        MainMenu.SetActive(false);
        SelectStage.SetActive(true);
        for(int i = 0; i < gameManager.stages.Count; i++)
        {
            if (gameManager.stages[i].isCleared)
            {
                SelectStage.transform.Find("stage" + (i + 1).ToString()).gameObject.GetComponent<Image>().color = Color.red;
            }
        }
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
        InGameMenu.transform.Find("Options").gameObject.SetActive(true);
        InGameMenu.transform.Find("MenuButton").gameObject.SetActive(false);
    }

    public void OnClickedResume()
    {
        gameManager.fsm.next = StateType.Resume;
        InGameMenu.transform.Find("Options").gameObject.SetActive(false);
        InGameMenu.transform.Find("MenuButton").gameObject.SetActive(true);
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
