using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StateType { LoadTitle, MainMenu, SelectStage, LoadStage, InGame, GameOver, InGameMenu, Retry, GiveUp, Resume, Clear, Result}

public class FSM  {

    public StateType current, next;

    public void StartCurrentState()
    {
        switch (current)
        {
            case StateType.LoadTitle:
                OnStateLoadTitle();
                break;
            case StateType.MainMenu:
                OnStateMainMenu();
                break;
            case StateType.SelectStage:
                OnStateSelectStage();
                break;
            case StateType.LoadStage:
                OnStateLoadStage();
                break;
            case StateType.InGame:
                OnStateInGame();
                break;
            case StateType.GameOver:
                OnStateGameOver();
                break;
            case StateType.InGameMenu:
                OnStateInGameMenu();
                break;
            case StateType.Retry:
                OnStateRetry();
                break;
            case StateType.GiveUp:
                OnStateGiveUp();
                break;
            case StateType.Resume:
                OnStateResume();
                break;
            case StateType.Clear:
                OnStateClear();
                break;
            case StateType.Result:
                OnStateResult();
                break;
        }
    }

    public void UpdateStates()
    {
        if (current != next)
        {
            current = next;
            Debug.Log("next states is " + current.ToString());
            StartCurrentState();
            Debug.Log("updateStates");
        }
    }
    

    #region OnStates
    void OnStateLoadTitle()
    {
        Debug.Log("LoadTitle");
        Object.DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene("MainMenu");
        Debug.Log("MainMenuSceneLoaded");
        next = StateType.MainMenu;
    }
    
    void OnStateMainMenu()
    {
        Debug.Log("MainMenu");
    }

    void OnStateSelectStage()
    {
        Debug.Log("SelectStage");
    }

    void OnStateLoadStage()
    {
        Debug.Log("LoadStage");
        Object.DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene("InGame");
        Debug.Log("InGameSceneLoaded");
        next = StateType.InGame;
    }

    void OnStateInGame()
    {
        Debug.Log("InGame");
        Time.timeScale = 1;
    }

    void OnStateGameOver()
    {
        Debug.Log("GameOver");
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("InGameMenu").gameObject.SetActive(false);
        GameObject fadeoutscreen = GameObject.Find("Canvas").transform.Find("FadeOut").gameObject;
        fadeoutscreen.SetActive(true);
        fadeoutscreen.GetComponent<FadeOut>().FadeOutStart();
        canvas.transform.Find("InGameUI").gameObject.SetActive(false);
        canvas.transform.Find("GameOver").gameObject.SetActive(true);
    }

    void OnStateInGameMenu()
    {
        Debug.Log("InGameMenu");
        Time.timeScale = 0;
    }

    void OnStateRetry()
    {
        Debug.Log("Retry");
        next = StateType.LoadStage;
		Time.timeScale = 1;
	}

    void OnStateGiveUp()
    {
        Debug.Log("GiveUp");
        next = StateType.LoadTitle;
		Time.timeScale = 1;
	}

    void OnStateResume()
    {
        Debug.Log("Resume");
        next = StateType.InGame;
    }

    void OnStateClear()
    {
        Debug.Log("Clear");
        StreamReader streamReader = new StreamReader("Assets\\Scripts\\MapList.txt");
        List<string> allstrs = new List<string>();
        string curLine = streamReader.ReadLine();
        int ClrStageNum = GameObject.Find("GameManager").GetComponent<GameManager>().stageNum + 1;
        while(curLine != null)
        {
            allstrs.Add(curLine);
            curLine = streamReader.ReadLine();
        }

        for(int i = 0; i < allstrs.Count; i++)
        {
            Debug.Log(allstrs[i]);
            if (allstrs[i].StartsWith("st" + ClrStageNum.ToString()) && !allstrs[i].EndsWith("C"))
            {
                allstrs[i] += " C";
                Debug.Log(allstrs[i]);
                break;
            }
        }

        streamReader.Close();
        Debug.Log("fileread");
        StreamWriter streamWriter = new StreamWriter("Assets\\Scripts\\MapList.txt", false);
        
        for(int i = 0; i < allstrs.Count; i++)
        {
            if(!allstrs[i].Equals(""))
                streamWriter.WriteLine(allstrs[i]);
        }

        streamWriter.Close();
        Debug.Log("filewrite");
        next = StateType.Result;
    }

    void OnStateResult()
    {
        Debug.Log("Result");
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("Finish").gameObject.SetActive(true);
        canvas.transform.Find("InGameMenu").gameObject.SetActive(false);
        canvas.transform.Find("InGameUI").gameObject.SetActive(false);
    }
    #endregion

}
