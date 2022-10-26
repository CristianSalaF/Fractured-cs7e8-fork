using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [FormerlySerializedAs("_setVolume")] [SerializeField] private SetSettings setSettings;
    
    //Unused code for a button that would restart the game
    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float vol)
    {
        setSettings.SaveVolumeLevel(1, vol);
    }

    public void SetMusicVolume(float vol)
    {
        setSettings.SaveVolumeLevel(2, vol);
    }

    public void SetSFXVolume(float vol)
    {
        setSettings.SaveVolumeLevel(3, vol);
    }
}
