using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOn : MonoBehaviour, IInteractable
{
    public GameObject obj;
    public AudioSource audioSource;
    public AudioClip[] TorchSounds;
    public void Start()
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Interact() {
        if (obj.CompareTag("Torch") && (obj.transform.GetChild(0).gameObject.activeSelf == false)) {
            audioSource.PlayOneShot(TorchSounds[UnityEngine.Random.Range(0, TorchSounds.Length - 1)]);
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
