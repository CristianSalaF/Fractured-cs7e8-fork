using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;

    private float _volumeVal;

    private void Awake()
    {
        //Debug.Log("Start SetVolume LoadVolumeLevel");
        LoadVolumeLevel(1);
        LoadVolumeLevel(2);
        LoadVolumeLevel(3);
        //Debug.Log("Ending SetVolume LoadVolumeLevel");
    }

    public void LoadVolumeLevel(int sliderNum)
    {
        switch (sliderNum)
        {
            case 1:
                ProcessVolumeLoad(_sliderMaster.transform.name, _sliderMaster);
                break;
                
            case 2:
                ProcessVolumeLoad(_sliderMusic.transform.name, _sliderMusic);
                break;

            case 3:
                ProcessVolumeLoad(_sliderSFX.transform.name, _sliderSFX);
                break;

            default:
                break;
        }
        //Debug.Log("SetVolume.cs, loading " + sliderName);
    }

    private void ProcessVolumeLoad(string sliderName, Slider slider)
    {
        //Debug.Log(sliderName + "'s PlayerPrefs loading returns: "+ PlayerPrefs.GetFloat(sliderName, 0.5f));

        _volumeVal = PlayerPrefs.GetFloat(sliderName, 0.5f);
        slider.value = _volumeVal;
        _mixer.SetFloat(sliderName, Mathf.Log10(_volumeVal) * 20);
    }

    public void SaveVolumeLevel(int sliderNum, float sliderVal)
    {
        switch (sliderNum)
        {
            case 1:
                //Debug.Log("Saving MasterVol");
                SaveSlider(sliderVal, _sliderMaster.transform.name);
                break;
                
            case 2:
                //Debug.Log("Saving MusicVol");
                SaveSlider(sliderVal, _sliderMusic.transform.name);
                break;

            case 3:
                //Debug.Log("Saving SFXVol");
                SaveSlider(sliderVal, _sliderSFX.transform.name);
                break;

            default:
                break;
        }
        //Debug.Log("SetVolume.cs, searching for " + sliderName);
    }

    private void SaveSlider(float sliderVal, string sliderName)
    {
        //Debug.Log(sliderName + ": Saving value " + sliderVal + " to PlayerPrefs");
        _volumeVal = sliderVal;
        _mixer.SetFloat(sliderName, Mathf.Log10(_volumeVal) * 20); 
        PlayerPrefs.SetFloat(sliderName, _volumeVal);
        //Debug.Log(sliderName + ": Saved" + PlayerPrefs.GetFloat(sliderName, 0.5f));
    }
}