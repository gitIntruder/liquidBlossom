using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
  [Header("Volume Setting")]
  [SerializeField] private TMP_Text volumeTextValue = null;
  [SerializeField] private Slider volumeSlider = null;
  [SerializeField] private float defaultVolume = 0.5F;

  [Header("Levels to Load")]
  public string newGameLevel;

  [Header("Graphics Settings")]
  [SerializeField] private Slider brightnessSlider = null;
  [SerializeField] private TMP_Text brightnessTextValue = null;
  [SerializeField] private float defaultBrightness = 1;

  private int _qualityLevel;
  private bool _isFullScreen;
  private float _brightnessLevel;

  [Header("Resolution Dropdowns")]
  public TMP_Dropdown resolutionDropdown;
  private Resolution[] resolutions;

  public void Start()
  {
    resolutions = Screen.resolutions;
    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

    int currentResolutionIndex = 0;

    for (int i = 0; i < resolutions.Length; i++)
    {
      string option = resolutions[i].width + " x " + resolutions[i].height;
      options.Add(option);

      if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
      {
        currentResolutionIndex = i;
      }
    }

    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIndex;
    resolutionDropdown.RefreshShownValue();
  }

  public void SetResolution(int resolutionIndex)
  {
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
  }

  public void NewGameDialogYes()
  {
    SceneManager.LoadScene(newGameLevel);
  }

  public void QuitButton()
  {
    Application.Quit();
  }

  public void SetVolume(float volume)
  {
    AudioListener.volume = volume;
    volumeTextValue.text = volume.ToString("0.0");
  }

  public void VolumeApply()
  {
    PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    // Show Prompt
  }

  public void SetBrightness(float brightness)
  {
    _brightnessLevel = brightness;
    brightnessTextValue.text = brightness.ToString("0.0");
  }

  public void SetFullScreen(bool isFullScreen)
  {
    _isFullScreen = isFullScreen;
  }

  public void SetQuality(int qualityIndex)
  {
    _qualityLevel = qualityIndex;
  }

  public void GraphicsApply()
  {
    PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
    //Change brightness with post processing or w/e

    PlayerPrefs.SetInt("masterQuality", _qualityLevel);
    QualitySettings.SetQualityLevel(_qualityLevel);

    PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
    Screen.fullScreen = _isFullScreen;
  }

  public void ResetButton(string MenuType)
  {
    if(MenuType == "Audio")
    {
      AudioListener.volume = defaultVolume;
      volumeSlider.value = defaultVolume;
      volumeTextValue.text = defaultVolume.ToString("0.0");
      VolumeApply();
    }
  }
}
