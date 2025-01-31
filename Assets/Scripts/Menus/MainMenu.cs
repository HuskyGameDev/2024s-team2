using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    public Slider sensitivitySlider;

    void Start()
    {
        float mouseSensitivity = PlayerPrefs.GetFloat("sensitivityVal");
        sensitivitySlider.value = mouseSensitivity / 10;
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        //Debug.Log("Player has quit the game");
    }

    public void AdjustSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivityVal", sensitivitySlider.value * 10);
        UnityEngine.Debug.Log(PlayerPrefs.GetFloat("sensitivityVal"));
    }
}
