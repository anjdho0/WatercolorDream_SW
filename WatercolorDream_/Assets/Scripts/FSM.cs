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
        current = next;
        Debug.Log("next states is " + current.ToString());
        StartCurrentState();
        Debug.Log("updateStates");
    }

    #region OnStates
    void OnStateLoadTitle()
    {
        Object.DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene("MainMenu");
        Debug.Log("MainMenuSceneLoaded");
        next = StateType.MainMenu;
    }
    
    void OnStateMainMenu()
    {
        
    }

    void OnStateSelectStage()
    {
        
    }

    void OnStateLoadStage()
    {
        Object.DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene("InGame");
        next = StateType.InGame;
    }

    void OnStateInGame()
    {

    }

    void OnStateGameOver()
    {
        
    }

    void OnStateInGameMenu()
    {

    }

    void OnStateRetry()
    {

    }

    void OnStateGiveUp()
    {

    }

    void OnStateResume()
    {

    }

    void OnStateClear()
    {

    }

    void OnStateResult()
    {

    }
    #endregion

}
