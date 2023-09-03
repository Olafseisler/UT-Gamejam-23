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
            AudioManager.PlayMusic(Music.sekrit);
            PlayerPrefs.SetInt("SecretEnd", 0);

            foreach (Transform child in transform) // get rid of blood for the "true" ending 
            {
                if (child.childCount > 0)
                {
                    child.GetChild(0).gameObject.SetActive(false);
                }
                if (child.name == "trueEnd")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
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
