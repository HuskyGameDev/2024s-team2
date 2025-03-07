using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatWaypointManager : MonoBehaviour
{
    public List<GameObject> batWaypointNodes;

    public Color gizmosColor;
    public bool isInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        batWaypointNodes = GameObject.FindGameObjectsWithTag("BatWaypointNode").ToList<GameObject>();
        isInstantiated = true;
    }

    // add_edge(G, x, y, z): adds the edge z from the vertex x to the vertex y, if it is not there; 
    public bool AddEdge(GameObject node1, GameObject node2) {
        BatWaypointNode node1Script = node1.GetComponent<BatWaypointNode>();
        BatWaypointNode node2Script = node2.GetComponent<BatWaypointNode>();

        // If they are already connected, do nothing.
        if (Adjacent(node1, node2)) { return false; }

        node1Script.connectedNodes.Add(node2);
        node2Script.connectedNodes.Add(node1);

        return true;
    }

    // remove_edge(G, x, y): removes the edge from the vertex x to the vertex y, if it is there; 
    public bool RemoveEdge(GameObject node1, GameObject node2) {
        BatWaypointNode node1Script = node1.GetComponent<BatWaypointNode>();
        BatWaypointNode node2Script = node2.GetComponent<BatWaypointNode>();

        bool removed1 = node1Script.connectedNodes.Remove(node2);
        bool removed2 = node2Script.connectedNodes.Remove(node1);

        return removed1 || removed2;
    }

     // adjacent(G, x, y): tests whether there is an edge from the vertex x to the vertex y;
    public bool Adjacent(GameObject node1, GameObject node2) {
        BatWaypointNode node1Script = node1.GetComponent<BatWaypointNode>();

        if (node1Script.connectedNodes.Contains(node2)) {
            return true;
        }
        return false;
    }

    // neighbors(G, x): lists all vertices y such that there is an edge from the vertex x to the vertex y;
    public List<GameObject> Neighbors(GameObject node) {
        BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();

        return nodeScript.connectedNodes;
    }

    // The heart of bat pathfinding technology. Finds the shortest path from one node to another.
    public GameObject[] DFS(GameObject from, GameObject to) {
        BatWaypointNode fromScript = from.GetComponent<BatWaypointNode>();
        BatWaypointNode toScript = to.GetComponent<BatWaypointNode>();

        GameObject[] path = null;

        return path;
    }

    /* THESE ARE UNNECESARY (but I did them anyway for whatever reason):

        // add_vertex(G, x): adds the vertex x, if it is not there;
        private bool AddVertex(GameObject node) {
            // BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();
            if (batWaypointNodes.Contains(node)) {
                return false;
            }
            batWaypointNodes.Add(node);
            return true;
        }

        // remove_vertex(G, x): removes the vertex x, if it is there;
        private bool RemoveVertex(GameObject node) {
            return batWaypointNodes.Remove(node);
        }

    */
}
