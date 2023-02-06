using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private PlayerControls playerControls;
    private InputAction menu;
    private EventSystem EVRef;
    [SerializeField] private GameObject selectedUIElement;
    [SerializeField] private GameObject exitButton; // to disable in webgl
    [SerializeField] private GameObject MobileUI; 
    private void Awake()
    {
        playerControls = new PlayerControls();
        EVRef = EventSystem.current; // get the current event system
    }

    private void Start()
    {
        
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
        if (Time.timeScale == 0f && !GameIsPaused)
        {
            Debug.Log("Game already paused, not able to pause again.");
            return;
        }
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
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.isMobilePlatform)
        {
            exitButton.SetActive(false);
        }
        if (Application.isMobilePlatform)
        {
            Debug.Log("mobile!");
            MobileUI.SetActive(false);
        }
        EVRef.SetSelectedGameObject(selectedUIElement);   // set current selected button
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        if (Application.isMobilePlatform)
        {
            Debug.Log("mobile!");
            MobileUI.SetActive(true);
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = !GameIsPaused;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
