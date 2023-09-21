using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeOut;
    [SerializeField] private GameObject exitButton; // to disable in webgl

    void Start()
    {
        AudioManager.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.2f));
        AudioManager.SetSoundVolume(PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        AudioManager.PlayMusic(Music.menu_music);
        PlayerPrefs.SetInt("SecretEnd", 0);
        if (BuildConstants.isWebGL || BuildConstants.isMobile || BuildConstants.isExpo)
        {
            exitButton.SetActive(false);
        }
    }

    public void PlayGame ()
    {
        AudioManager.FadeMusicOut(1);
        fadeOut.SetActive(true);
        StartCoroutine(DelaySceneLoad(2, "OpeningCutscene"));
    }
    public void OpenCredits ()
    {
        AudioManager.FadeMusicOut(1);
        fadeOut.SetActive(true);
        StartCoroutine(DelaySceneLoad(2, "Credits"));
    }
    IEnumerator DelaySceneLoad(float delay, string scene)
    {
        AudioManager.FadeMusicOut(delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}