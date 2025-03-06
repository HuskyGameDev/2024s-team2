using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWaypointManager : MonoBehaviour
{
    public GameObject[] batWaypointNodes;

    public Color gizmosColor;
    public bool isInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        batWaypointNodes = GameObject.FindGameObjectsWithTag("BatWaypointNode");
        isInstantiated = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
