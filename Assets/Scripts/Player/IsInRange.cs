using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsInRange : MonoBehaviour
{
    private Camera playerCam;

    RaycastHit hit;

    //default crosshair
    public RawImage crosshair_default;

    //red crosshair
    public RawImage crosshair_red;


    // Start is called before the first frame update
    void Start()
    {
        //initialize camera
        playerCam = Camera.main;

        //disable red crosshair by default
        crosshair_red.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if enemy is in range
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 3f))
        {
            //if raycast detects enemy
            if(hit.collider.tag == "Enemy")
            {
                //hide default, show red
                crosshair_red.enabled = true;
                crosshair_default.enabled = false;
            }
            else
            {
                //hide red show default
                crosshair_default.enabled = true;
                crosshair_red.enabled = false;
            }
            
        }
    }
}
