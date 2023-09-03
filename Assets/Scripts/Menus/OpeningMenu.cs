using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class OpeningMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField] private GameObject openingMenuUI;
    [SerializeField] private GameObject helpUI;
    [SerializeField] private GameObject impostor_monke;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer sp_renderer;
    [SerializeField] private GameObject MobileUI;
    public GameObject selectedUIElement;
    private EventSystem EVRef;
    private PlayerControls playerControls;
    private InputAction menu;

    private void Awake()
    {
        playerControls = new PlayerControls();
        GameIsPaused = true;
        EVRef = EventSystem.current; // get the current event system
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
        Debug.Log("PAUSING: " + GameIsPaused);
        player.SetActive(true);
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
        MobileUI.SetActive(false);
        openingMenuUI.SetActive(true);
        EVRef.SetSelectedGameObject(selectedUIElement);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        openingMenuUI.SetActive(false);
        helpUI.SetActive(false);
        Time.timeScale = 1f;
        MobileUI.SetActive(true);
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
