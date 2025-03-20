using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class TorchInteract : MonoBehaviour
{
    public float InteractRange = 4f;

    void Update()
    {
        Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Input.GetKeyDown(KeyCode.F)) {
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) {
                Debug.Log("Hit");
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    Debug.Log("Smaller loop");
                    interactObj.Interact();
                }
            }
        }
    }
}
