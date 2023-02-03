using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
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


    public void Pause(InputAction.CallbackContext context)
    {
        GameIsPaused = !GameIsPaused;
        if (GameIsPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
