using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using JSAM;

public class Credits : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction menu;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        menu = playerControls.Menu.Escape;
        menu.Enable();
        menu.performed += Pause;
    }
    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context) // we're just going to skip credits when esc is pressed
    {
        OnCreditsEnd();
    }

    private void Start()
    {
        AudioManager.PlayMusic(Music.credits_song);
    }

    // Start is called before the first frame update
    public void OnCreditsEnd()
    {
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        AudioManager.FadeMusicOut(1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
