using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWaypointNode : MonoBehaviour
{
    public BatWaypointManager batWaypointManager;
    public bool hideGizmo = true;

    public BatWaypointNode[] connectedNodes;

    private void OnDrawGizmos() {
        if (!hideGizmo) { return;}
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        foreach (BatWaypointNode node in connectedNodes) {
            if (!node) {continue;}
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
