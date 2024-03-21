using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverSounds : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioClip hover;

    public void HoverSound()
    {
        buttonSound.PlayOneShot(hover);
    }
}
