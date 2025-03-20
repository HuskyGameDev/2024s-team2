using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchOn : MonoBehaviour, IInteractable
{
    public GameObject obj;
    public void Start()
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Interact() {
        if (obj.CompareTag("Torch")) {
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
