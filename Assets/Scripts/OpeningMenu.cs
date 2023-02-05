using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class OpeningMenu : MonoBehaviour
{
    public static bool GameIsPaused = true;
    [SerializeField] private GameObject openingMenuUI;
    [SerializeField] private GameObject impostor_monke;
    [SerializeField] private GameObject collision_box; // the box we lock monke in
    [SerializeField] private SpriteRenderer sp_renderer;
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
        Time.timeScale = 1f;
    }

    public void OnPauseButton(InputAction.CallbackContext context)
    {
        Pause();
    }

    public void Pause()
    {
        GameIsPaused = !GameIsPaused;
        sp_renderer.enabled = true;
        collision_box.SetActive(false);
        impostor_monke.SetActive(false);
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
        openingMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        openingMenuUI.SetActive(false);
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
