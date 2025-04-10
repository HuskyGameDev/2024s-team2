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
        if(PlayerPrefs.GetFloat("sensitivityVal") == 0)
        {
            PlayerPrefs.SetFloat("sensitivityVal", 200);
            PlayerPrefs.SetInt("maxHold1", 10);
            PlayerPrefs.SetInt("maxHold2", 8);
            PlayerPrefs.SetInt("maxHold3", 5);
            PlayerPrefs.SetInt("maxHold4", 5);
            PlayerPrefs.SetInt("maxHold5", 8);
            PlayerPrefs.SetInt("maxHold6", 5);
            PlayerPrefs.SetInt("maxHold7", 3);
            PlayerPrefs.SetInt("maxHold8", 5);
            PlayerPrefs.SetInt("maxHold9", 5);
            PlayerPrefs.SetInt("maxHold10", 3);
        }
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MenuMusic>().PlayMusic();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        //reset all playerprefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("sensitivityVal", 200);
        PlayerPrefs.SetInt("maxHold1", 10);
        PlayerPrefs.SetInt("maxHold2", 8);
        PlayerPrefs.SetInt("maxHold3", 5);
        PlayerPrefs.SetInt("maxHold4", 5);
        PlayerPrefs.SetInt("maxHold5", 8);
        PlayerPrefs.SetInt("maxHold6", 5);
        PlayerPrefs.SetInt("maxHold7", 3);
        PlayerPrefs.SetInt("maxHold8", 5);
        PlayerPrefs.SetInt("maxHold9", 5);
        PlayerPrefs.SetInt("maxHold10", 3);
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
