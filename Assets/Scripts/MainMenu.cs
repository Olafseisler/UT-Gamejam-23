using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
        AudioManager.SetSoundVolume(PlayerPrefs.GetFloat("SFXVolume", 0.75f));
        AudioManager.PlayMusic(Music.menu_music);
    }

    public void PlayGame ()
    {
        AudioManager.FadeMusicOut(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenCredits ()
    {
        AudioManager.FadeMusicOut(1);
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
