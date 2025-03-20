using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource bgm;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        bgm = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (bgm.isPlaying)
        {
            return;
        }
        bgm.Play();
    }

    public void StopMusic()
    {
        bgm.Stop();
    }
}
