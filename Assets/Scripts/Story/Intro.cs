using JSAM;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] ParticleSystem gold_ps;
    [SerializeField] ParticleSystem cash_ps;
    public GameObject fadeOut;
    private InputAction interactAction;
    private void Awake()
    {
        var playerControls = new PlayerControls();
        interactAction = playerControls.Menu.Interact;
        AudioManager.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.2f));
        AudioManager.SetSoundVolume(PlayerPrefs.GetFloat("SFXVolume", 0.5f));
       if (BuildConstants.isMobile) Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value; // fix mobile FPS
    }

    private void OnEnable()
    {
        interactAction.performed += OnSkipPressed;
        interactAction.Enable();
    }
    private void OnDisable()
    {
        interactAction.performed -= OnSkipPressed;
        interactAction.Disable();
    }
    private void OnSkipPressed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnIntroEnd()
    {
        StartCoroutine(IntroEnd());
    }


    IEnumerator IntroEnd()
    {
        gold_ps.Play();
        cash_ps.Play();
        AudioManager.PlaySound(Sounds.intro_boom);
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
