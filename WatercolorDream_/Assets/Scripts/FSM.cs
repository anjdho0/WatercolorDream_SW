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
        GameObject.Find("Canvas").transform.Find("InGameMenu").gameObject.SetActive(false);
        GameObject fadeoutscreen = GameObject.Find("Canvas").transform.Find("FadeOut").gameObject;
        fadeoutscreen.SetActive(true);
        fadeoutscreen.GetComponent<FadeOut>().FadeOutStart();
        GameObject.Find("Canvas").transform.Find("GameOver").gameObject.SetActive(true);
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
    }

    void OnStateGiveUp()
    {
        Debug.Log("GiveUp");
        next = StateType.LoadTitle;
    }

    void OnStateResume()
    {
        Debug.Log("Resume");
        next = StateType.InGame;
    }

    void OnStateClear()
    {
        Debug.Log("Clear");
        //StreamWriter streamWriter = new StreamWriter("Assets\\Scripts\\MapListCopy.txt");
        next = StateType.Result;
    }

    void OnStateResult()
    {
        Debug.Log("Result");
        GameObject.Find("Canvas").transform.Find("Finish").gameObject.SetActive(true);
    }
    #endregion

}
