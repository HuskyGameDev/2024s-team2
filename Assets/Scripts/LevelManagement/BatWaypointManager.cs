using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public GameObject[] BFS(GameObject from, GameObject to, bool debug = true) {
        BatWaypointNode fromScript = from.GetComponent<BatWaypointNode>();
        BatWaypointNode toScript = to.GetComponent<BatWaypointNode>();

        Queue<GameObject> queue = new Queue<GameObject>();
        HashSet<GameObject> visited = new HashSet<GameObject>();
        List<GameObject> orderedVisit = new List<GameObject>();

        visited.Add(from);
        orderedVisit.Add(from);
        queue.Enqueue(from);
        fromScript.parent = null;

        while (queue.Count > 0) {
            GameObject u = queue.Dequeue();
            List<GameObject> connectedNodes = Neighbors(u);
            foreach (GameObject node in connectedNodes) {
                if (!visited.Contains(node)) {
                    visited.Add(node);
                    queue.Enqueue(node);
                    orderedVisit.Add(node);
                    BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();
                    nodeScript.parent = u; // This lets us traceback
                }
            }
        }
        
        GameObject[] path = backtrace(from, to);
        
        if (debug && Application.isEditor) {
            float pathLifeTime = 2f;
            float startEndHeight = 5f;
            float pathArrowHeight = 0.8f;
            GameObject first = path[0];
            GameObject last = path[path.Length-1];

            // Draw little spires at the beginning and end, green and red
            Debug.DrawLine(first.transform.position-Vector3.up*startEndHeight/2, first.transform.position+Vector3.up*startEndHeight/2, Color.green, pathLifeTime);
            Debug.DrawLine(last.transform.position-Vector3.up*startEndHeight/2, last.transform.position+Vector3.up*startEndHeight/2, Color.red, pathLifeTime);

            // Draw path for debugging
            for (int i = 0; i < path.Length-1; i++) {
                GameObject node = path[i];
                GameObject nextNode = path[i+1];
                Color lineColor = Color.white;
                Debug.DrawLine(node.transform.position + Vector3.up*pathArrowHeight, nextNode.transform.position, lineColor, pathLifeTime);
                Debug.DrawLine(node.transform.position + Vector3.down*pathArrowHeight, nextNode.transform.position, lineColor, pathLifeTime);
            }
        }

        Debug.Log("Pathing complete!");
        return path;
    }

    private GameObject[] backtrace(GameObject start, GameObject final) {
        List<GameObject> path = new List<GameObject>();
        BatWaypointNode finalScript = final.GetComponent<BatWaypointNode>();
        BatWaypointNode cursor = finalScript;
        while (cursor != null) {
            // Debug.Log(cursor.ToString());
            path.Add(cursor.gameObject);
            if (cursor == start || cursor.parent == null) {
                break;
            }
            cursor = cursor.parent.GetComponent<BatWaypointNode>();
        }
        path.Reverse();
        return path.ToArray();
    }

    public GameObject GetClosestNode(Vector3 position) {
        GameObject closestNode = null;
        float smallestDistance = Mathf.Infinity;
        foreach (GameObject node in batWaypointNodes) {
            float distance = Vector3.Distance(node.transform.position, position);
            if (distance < smallestDistance) {
                smallestDistance = distance;
                closestNode = node;
            }
        }
        return closestNode;
    }

    // remove_vertex(G, x): removes the vertex x, if it is there;
    public bool DeleteNode(GameObject node) {
        BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();

        GameObject[] connected = new GameObject[nodeScript.connectedNodes.Count];
        nodeScript.connectedNodes.CopyTo(connected);
        Debug.Log(connected.ToCommaSeparatedString());
        for (int i = 0; i < connected.Length; i++) {
            Debug.Log("Deleting connection to: " + i);
            RemoveEdge(node, connected[i]);
        }

        // Remove from manager list
        Debug.Log("Removing from waypoint list now");
        batWaypointNodes.Remove(node);

        // Destroy gameObject
        Destroy(node);

        return true;
    }

    /* THESE ARE UNNECESARY (but I did them anyway for whatever reason):

        // add_vertex(G, x): adds the vertex x, if it is not there;
        private bool AddNode(GameObject node) {
            // BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();
            if (batWaypointNodes.Contains(node)) {
                return false;
            }
            batWaypointNodes.Add(node);
            return true;
        }

    */
}
