using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenuCanvas;
    public Slider sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        float mouseSensitivity = PlayerPrefs.GetFloat("sensitivityVal");
        sensitivitySlider.value = mouseSensitivity / 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        paused = true;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        paused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void AdjustSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivityVal", sensitivitySlider.value * 10);
        UnityEngine.Debug.Log(PlayerPrefs.GetFloat("sensitivityVal"));
    }
}
