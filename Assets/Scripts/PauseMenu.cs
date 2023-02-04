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
        menu.performed += OnPauseButton;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void OnPauseButton(InputAction.CallbackContext context)
    {
        Pause();
    }

    public void Pause()
    {
        GameIsPaused = !GameIsPaused;
        if (GameIsPaused)
        {
            ActivateMenu();
        }
        else
        {
            Transform help = pauseMenuUI.transform.Find("HelpMenu");
            if (help.gameObject.activeSelf)
            {
                GameIsPaused = !GameIsPaused; // dont actually unpause game
                help.gameObject.SetActive(false);
                pauseMenuUI.transform.Find("PauseMenuButtons").gameObject.SetActive(true);           
            }
            else
            {
                DeactivateMenu();
            }
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
        GameIsPaused = !GameIsPaused;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
