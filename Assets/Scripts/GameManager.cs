using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Running,
    Sacrifice,
    Win,
    Lose,
}

public class GameManager : MonoBehaviour
{
    private GameState _currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        OnGameStateChanged(GameState.Start);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameStateChanged(GameState newState)
    {
        _currentState = newState;

        switch (_currentState)
        {
            case GameState.Start:
                //TODO: Setup stuff for starting game
                break;
            case GameState.Sacrifice:
                //TODO: Open sacrifice animal dialog
                break;
            case GameState.Running:
                //TODO: Handle running animation etc.
                break;
            case GameState.Lose:
                //TODO: Handle loss
                break;
            case GameState.Win:
                //TODO: Handle win
                break;
            default:
                Debug.Log("Unknown state passed to state manager");
                break;
        }
    }
    
    void HandleStart(){}
    
    void HandleRunning(){}
    
    void HandleSacrifice(){}
    
    void HandleLoss(){}
    
    void HandleWin(){}
    
}
