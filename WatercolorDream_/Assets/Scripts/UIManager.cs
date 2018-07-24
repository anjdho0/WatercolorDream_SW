using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject MainMenu, SelectStage, InGameMenu, Finish, C_param, M_param, Y_param, InGameUI, destdot, playerdot;

    GameManager gameManager;
    bool resultUpdated;

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        resultUpdated = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.fsm.current == StateType.Result && !resultUpdated)
        {
            ScoreUpdate();
            CubeUpdate();
            resultUpdated = true;
        }

        if (gameManager.fsm.current == StateType.InGame)
        {
            ParameterUpdate();
        }
	}

    void ScoreUpdate()
    {
        if(gameManager.score > 0.99f)
        {
            Finish.transform.Find("Result/ResultState.Complete").gameObject.SetActive(true);
        }
        else if (gameManager.score > 0.7f)
        {
            Finish.transform.Find("Result/ResultState/Clear").gameObject.SetActive(true);
        }
        else
        {
            Finish.transform.Find("Result/ResultState/Failed").gameObject.SetActive(true);
        }
        Finish.transform.Find("Result/Score").GetComponent<Text>().text = "Result\n" + Mathf.Round(gameManager.score * 100).ToString() + "%";
    }

    void ParameterUpdate()
    {
        Player player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        C_param.GetComponent<Slider>().value = player.cmyk.c;
        M_param.GetComponent<Slider>().value = player.cmyk.m;
        Y_param.GetComponent<Slider>().value = player.cmyk.y;

        C_param.transform.Find("Next").GetComponent<Slider>().value = player.nextcmyk.c;
        M_param.transform.Find("Next").GetComponent<Slider>().value = player.nextcmyk.m;
        Y_param.transform.Find("Next").GetComponent<Slider>().value = player.nextcmyk.y;
    }

    void CubeUpdate()
    {
        CMYK players = GameObject.Find("Player(Clone)").GetComponent<Player>().cmyk;
        CMYK dests = gameManager.dest;
        destdot.transform.position += new Vector3((-1) * dests.c * 100, dests.m * 100, (-1) * dests.y * 100);
        playerdot.transform.position += new Vector3((-1) * players.c * 100, players.m * 100, (-1) * players.y * 100);
    }

    public void OnClickedSelectStageButton(GameObject button)
    {
        if (button.transform.parent.name.Equals("Result"))
        {
            SceneManager.LoadScene("MainMenu");
            gameManager.fsm.next = StateType.LoadTitle;
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
        InGameUI.SetActive(false);
    }

    public void OnClickedResume()
    {
        gameManager.fsm.next = StateType.Resume;
        InGameMenu.transform.Find("Options").gameObject.SetActive(false);
        InGameMenu.transform.Find("MenuButton").gameObject.SetActive(true);        
        InGameUI.SetActive(true);
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
