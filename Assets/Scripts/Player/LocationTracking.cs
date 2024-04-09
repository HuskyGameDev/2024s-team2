using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTracking : MonoBehaviour
{
    string location;
    void Start()
    {
        location = "StartingRoom";
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "StartingRoom")
        {
            location = "StartingRoom";
            print(location);
        }

        if (collision.gameObject.tag == "BossRoom")
        {
            location = "BossRoom";
            print(location);
        }

        if (collision.gameObject.tag == "Room2")
        {
            location = "Room2";
            print(location);
        }

        if (collision.gameObject.tag == "Room3")
        {
            location = "Room3";
            print(location);
        }

        if (collision.gameObject.tag == "Room4")
        {
            location = "Room4";
            print(location);
        }

        if (collision.gameObject.tag == "Room5")
        {
            location = "Room5";
            print(location);
        }

        if (collision.gameObject.tag == "Room6")
        {
            location = "Room6";
            print(location);
        }
    }
}
