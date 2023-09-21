using System.Collections.Generic;
using UnityEngine;
using JSAM;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;
    public Button backButton;
    [SerializeField] GameObject resolutionOption;
    private PlayerControls playerControls;
    private InputAction settingsBack;
    private void Start()
    {
        playerControls = new PlayerControls();
        if (BuildConstants.isWebGL)
        {
            resolutionOption.SetActive(false);
        }
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new();
        int currentResolutionIndex = 0;
        for (int i=0; i < resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + Mathf.Round((float)resolutions[i].refreshRateRatio.value) + "hz";
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        sfxSlider.value = AudioManager.GetSoundVolume();
        musicSlider.value = AudioManager.GetMusicVolume();
        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = true;
        }
        settingsBack = playerControls.Menu.MenuBack;
        settingsBack.Enable();
        settingsBack.started += OnBack;
    }
    public void SetSFXVolume (float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        AudioManager.SetSoundVolume(volume);
    }
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        AudioManager.SetMusicVolume(volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void OnBack(InputAction.CallbackContext context)
    {
        backButton.onClick.Invoke();
    }

}