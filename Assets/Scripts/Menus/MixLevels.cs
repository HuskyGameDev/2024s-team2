using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public AudioMixer masterMixer;

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
