using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using JSAM;

public class Credits : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction escape;
    private InputAction interact;
    public GameObject fadeOut;
    private bool secretEnd = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        escape = playerControls.Menu.Escape;
        escape.Enable();
        interact = playerControls.Menu.Interact;
        interact.Enable();
        escape.performed += Pause;
        interact.performed += Pause;
    }
    private void OnDisable()
    {
        escape.Disable();
        interact.Disable();
    }

    public void Pause(InputAction.CallbackContext context) // we're just going to skip credits when esc is pressed
    {
        OnCreditsEnd();
    }

    private void Start()
    {
        secretEnd = PlayerPrefs.GetInt("SecretEnd", 0) == 1 ? true : false;
        if (secretEnd)
        {
            AudioManager.PlayMusic(Music.sekrit);
            PlayerPrefs.SetInt("SecretEnd", 0);
        }
        else
        {
            AudioManager.PlayMusic(Music.credits_song);
        }
    }

    // Start is called before the first frame update
    public void OnCreditsEnd()
    {
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        fadeOut.SetActive(true);
        AudioManager.FadeMusicOut(5);
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("MainMenu");
    }
}
