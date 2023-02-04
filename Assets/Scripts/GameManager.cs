using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;

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
                Debug.Log($"switched game state to start");
                player.position = startPos.position;
                OnGameStateChanged(GameState.Running);
                break;
            case GameState.Sacrifice:
                //TODO: Open sacrifice animal dialog
                Debug.Log($"switched game state to sacrifice");

                break;
            case GameState.Running:
                //TODO: Handle running animation etc.
                Debug.Log($"switched game state to running");

                break;
            case GameState.Lose:
                //TODO: Handle loss
                Debug.Log($"switched game state to loss");

                break;
            case GameState.Win:
                //TODO: Handle win
                        Debug.Log($"switched game state to win");

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
