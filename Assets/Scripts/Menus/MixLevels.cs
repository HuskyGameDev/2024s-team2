using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public AudioMixer masterMixer;
    public int setSlider;

    void Start()
    {
        float value = 0;
        switch (setSlider)
        {
            case (1):
                masterMixer.GetFloat("MasterVol", out value);
                break;
            case (2):
                masterMixer.GetFloat("MusicVol", out value);
                break;
            case (3):
                masterMixer.GetFloat("SFXVol", out value);
                break;
            default:
                masterMixer.GetFloat("MasterVol", out value);
                break;
        }
        volumeSlider.value = value;
    }

    public void SetMasterLevel()
    {
        float masterLevel = volumeSlider.value;
        masterMixer.SetFloat("MasterVol", masterLevel);
        Debug.Log("master value: " + masterLevel);
    }

    public void SetSFXLevel()
    {
        float sfxLevel = volumeSlider.value;
        masterMixer.SetFloat("SFXVol", sfxLevel);
        Debug.Log("sfx value: " + sfxLevel);
    }

    public void SetMusicLevel()
    {
        float musicLevel = volumeSlider.value;
        masterMixer.SetFloat("MusicVol", musicLevel);
        Debug.Log("music value: " + musicLevel);
    }
}
