using System.Collections;
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
    public GameObject normalCredits;
    public GameObject secretCredits;
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
        secretEnd = PlayerPrefs.GetInt("SecretEnd", 0) == 1;
        if (secretEnd)
        {
            normalCredits.SetActive(false);
            secretCredits.SetActive(true);
            AudioManager.PlayMusic(Music.sekrit);
            PlayerPrefs.SetInt("SecretEnd", 0);

        }
        else
        {
            normalCredits.SetActive(true);
            secretCredits.SetActive(false);
            AudioManager.PlayMusic(Music.credits_song_remaster);
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
        AudioManager.FadeMusicOut(1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
