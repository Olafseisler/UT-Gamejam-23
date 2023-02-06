using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using JSAM;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameObject sacrificeDialog;
    [SerializeField] public GameObject fadeOut;
    [SerializeField] public HuntPlayer enemyScript;
    [SerializeField] private BloodParticle playerBlood;
    [SerializeField] private GameObject MobileUI;
    private GameState _currentState;
    private int _currentMoney = 10000;
    private int previous_song_pos = 0; // in samples

    private EventSystem EVRef;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            Debug.Log("mobile!");
            MobileUI.SetActive(true);
        }
        EVRef = EventSystem.current; // get the current event system
        OnGameStateChanged(GameState.Start);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnGameStateChanged(GameState newState)
    {
        if (_currentState.Equals(GameState.Win) || _currentState.Equals(GameState.Lose))
        {
            Debug.Log("Tried to change state after hitting an ending, ignoring.");
            return;
        }
        _currentState = newState;
        switch (_currentState)
        {
            case GameState.Start:
                //TODO: Setup stuff for starting game
                HandleStart();
                break;
            case GameState.Sacrifice:
                //TODO: Open sacrifice animal dialog
                Debug.Log($"switched game state to sacrifice");
                HandleSacrifice();
                break;
            case GameState.Running:
                //TODO: Handle running animation etc.
                Debug.Log($"switched game state to running");
                HandleRunning();
                break;
            case GameState.Lose:
                //TODO: Handle loss
                HandleLoss();
                Debug.Log($"switched game state to loss");
                break;
            case GameState.Win:
                //TODO: Handle win
                HandleWin();
                Debug.Log($"switched game state to win");
                break;
            default:
                Debug.Log("Unknown state passed to state manager");
                break;
        }
    }

    void HandleStart()
    {
        Debug.Log($"switched game state to start");
        PlayerPrefs.SetInt("Score", 0);
        player.position = startPos.position;
        AudioManager.PlayMusic(Music.chase_music);
        OnGameStateChanged(GameState.Running);
    }

    void HandleRunning()
    {
        UnpauseGame();
    }

    void HandleSacrifice()
    {
        previous_song_pos = AudioManager.GetMusicPlaybackPosition();
        AudioManager.StopMusic();
        AudioManager.PlayMusic(Music.sacrifice_music);
        PauseGame();
        sacrificeDialog.SetActive(true);


        EVRef.SetSelectedGameObject(sacrificeDialog.transform.GetChild(0).transform.GetChild(0)
            .gameObject); // set current selected button
    }

    void HandleLoss()
    {
        Debug.Log("Your money ran out! Lost game!");
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene("BadEnd");
        playerBlood.ShowBlood();
        StartCoroutine(DelaySceneLoad(2, "BadEnd"));
    }

    void HandleWin()
    {
        PlayerPrefs.SetInt("Score", _currentMoney);
        //SceneManager.LoadScene(5);
        StartCoroutine(DelaySceneLoad(2, "GoodEnd"));
    }

    IEnumerator DelaySceneLoad(float delay, string scene)
    {
        fadeOut.SetActive(true);
        AudioManager.FadeMusicOut(delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void AddMoney(int moneyToAdd)
    {
        _currentMoney += moneyToAdd;
    }

    public void HandleCommitSacrifice(int moneyToRemove)
    {
        _currentMoney -= moneyToRemove;
        if (_currentMoney <= 0)
        {
            OnGameStateChanged(GameState.Lose);
        }

        Debug.Log("Saved you this time! Money left:" + _currentMoney);
        // player.position = player.position + new Vector3(5f, 0, 0);

        sacrificeDialog.SetActive(false);
        //OnGameStateChanged(GameState.Running);
    }

    public void handleEnemySlowdown(float slowDown)
    {
        enemyScript.slowEnemyDown(slowDown);
    }

    public int GetMoney()
    {
        return _currentMoney;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        AudioManager.StopMusic();
        AudioManager.PlayMusic(Music.chase_music);
        //AudioManager.PauseMusic();
        AudioManager.SetMusicPlaybackPosition(previous_song_pos);
        //AudioManager.ResumeMusic();
    }
}