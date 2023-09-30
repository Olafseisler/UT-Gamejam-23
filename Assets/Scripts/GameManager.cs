using System.Collections;
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
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private HuntPlayer enemyScript;
    [SerializeField] private BloodParticle playerBlood;
    [SerializeField] private GameObject MobileUI;
    [SerializeField] GameObject tutorialUI;
    bool isFirstTime;
    private SacrificeController sacrificeController;
    private GameState _currentState;
    private int _currentMoney = 15000;
    private int previous_song_pos = 0; // in samples

    private EventSystem EVRef;

    // Start is called before the first frame update
    void Start()
    {
        if (BuildConstants.isExpo)
        {
            isFirstTime = true;
        }
        else
        {
            isFirstTime = PlayerPrefs.GetInt("isFirstTime", 1) == 1;
        }
        MobileUI.SetActive(true);
        sacrificeController = GetComponent<SacrificeController>();
        EVRef = EventSystem.current; // get the current event system
        OnGameStateChanged(GameState.Start);
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
                HandleStart();
                break;
            case GameState.Sacrifice:
                Debug.Log($"switched game state to sacrifice");
                HandleSacrifice();
                break;
            case GameState.Running:
                Debug.Log($"switched game state to running");
                HandleRunning();
                break;
            case GameState.Lose:
                HandleLoss();
                Debug.Log($"switched game state to loss");
                break;
            case GameState.Win:
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
        PlayerPrefs.SetInt("SecretEnd", 0);
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
        MobileUI.SetActive(false);
        previous_song_pos = AudioManager.GetMusicPlaybackPosition();
        AudioManager.StopMusic();
        AudioManager.PlayMusic(Music.sacrifice_music);
        PauseGame();
        sacrificeController.UpdatePrices();
        sacrificeDialog.SetActive(true);
        if (isFirstTime && tutorialUI != null)
        {
            tutorialUI.SetActive(true);
        }

        EVRef.SetSelectedGameObject(sacrificeDialog.transform.GetChild(0).transform.GetChild(0)
            .gameObject); // set current selected button
    }

    void HandleLoss()
    {
        Debug.Log("Your money ran out! Lost game!");
        Time.timeScale = 1.0f;
        playerBlood.ShowBlood();
        StartCoroutine(DelaySceneLoad(2, "BadEnd"));
    }

    void HandleWin()
    {
        PlayerPrefs.SetInt("Score", _currentMoney);
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

    public float HandleCommitSacrifice(int moneyToRemove, float sacrificeMultiplier)
    {
        float to_remove = moneyToRemove * sacrificeMultiplier;
        _currentMoney -= (int)to_remove;
        if (_currentMoney <= 0)
        {
            OnGameStateChanged(GameState.Lose);
        }

        Debug.Log("Saved you this time! Money left:" + _currentMoney);
        sacrificeDialog.SetActive(false);
        if (isFirstTime && tutorialUI != null)
        {
            PlayerPrefs.SetInt("isFirstTime", 0);
            tutorialUI.SetActive(false);
            isFirstTime = false;
        }
        return sacrificeMultiplier + 0.5f;
    }

    public void HandleEnemySlowdown(float slowDown)
    {
        enemyScript.SlowEnemyDown(slowDown);
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
        AudioManager.SetMusicPlaybackPosition(previous_song_pos);
    }
}