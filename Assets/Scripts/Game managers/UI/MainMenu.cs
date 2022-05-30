using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SetVolume _setVolume;
    [SerializeField] private SetLanguage _setLanguage;
    

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float vol)
    {
        _setVolume.SaveVolumeLevel(1, vol);
    }

    public void SetMusicVolume(float vol)
    {
        _setVolume.SaveVolumeLevel(2, vol);
    }

    public void SetSFXVolume(float vol)
    {
        _setVolume.SaveVolumeLevel(3, vol);
    }

    public void ToggleLanguage()
    {
        _setLanguage.ToggleLang();
    }
}
