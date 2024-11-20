using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTorch : MonoBehaviour
{
    bool active = false;
    GameObject obj;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && active == true)
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable")) {
            obj = other.gameObject;
            active = true;
        }
        else {
            active = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
    }
}
