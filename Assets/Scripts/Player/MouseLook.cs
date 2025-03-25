using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f; // Used to scale the sensitivity factor
    public Transform orientation; // This script must have a transform to update with respect to the mouse input. 
    float xRotation = 0f; // Helps limit looking up and down so you cannot make the camera do flips
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide the cursor
    }

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("sensitivityVal");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Adjusting this by delta time keeps the movement at the same rate
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //     so that it does not change with framerate changes
        
        yRotation += mouseX;
        xRotation -= mouseY; // Use the y input of the mouse to update the x rotation of the camera
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit the up and down rotation TODO this is not working 
        
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0); // Rotate the camera
        orientation.rotation = Quaternion.Euler(0,yRotation,0); // Rotate the players body
    }
}
