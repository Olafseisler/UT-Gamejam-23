using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
using UnityEngine.InputSystem;
public class BadEnding : MonoBehaviour
{
    private bool wasSkipEndingPressed = false;
    private PlayerControls playerControls;
    private InputAction menu;
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.StopMusic();
        //AudioManager.PlayMusic();
        StartCoroutine(SilenceForTheFallen());
    }
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
        wasSkipEndingPressed = true;
        Debug.Log("pressed");
    }

    IEnumerator SilenceForTheFallen()
    {
        for (float timer = 6; timer >= 0; timer -= Time.deltaTime)
        {
            if (wasSkipEndingPressed)
            {
                yield break;
            }
            yield return null;
        }
        // TODO - it doesnt get here for some reason
        AudioManager.StopMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
