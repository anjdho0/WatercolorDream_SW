using System.Collections;
using System.Collections.Generic;
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
    }

    void OnStateGameOver()
    {
        Debug.Log("GameOver");
    }

    void OnStateInGameMenu()
    {
        Debug.Log("InGameMenu");
        Time.timeScale = 0;
    }

    void OnStateRetry()
    {
        Debug.Log("Retry");
        Time.timeScale = 1;
        next = StateType.LoadStage;
    }

    void OnStateGiveUp()
    {
        Debug.Log("GiveUp");
        Time.timeScale = 1;
        next = StateType.LoadTitle;
    }

    void OnStateResume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
    }

    void OnStateClear()
    {

    }

    void OnStateResult()
    {

    }
    #endregion

}
