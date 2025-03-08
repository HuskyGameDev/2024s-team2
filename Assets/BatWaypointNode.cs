using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWaypointNode : MonoBehaviour
{
    public BatWaypointManager batWaypointManager;
    public bool hideGizmo = true;
    public GameObject parent;

    public List<GameObject> connectedNodes = new List<GameObject>();

    private void OnDrawGizmos() {
        
        if (!hideGizmo) { return;}
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        foreach (GameObject nodeObj in connectedNodes) {
            if (nodeObj == null) { continue; }
            BatWaypointNode node = nodeObj.GetComponent<BatWaypointNode>();
            if (node == null) { continue; }
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
    }
}
