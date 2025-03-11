using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;

[CustomEditor(typeof(BatWaypointNode)), CanEditMultipleObjects]
public class WaypointEditor : Editor
{
    public BatWaypointManager batWaypointManager;

    public bool hideGizmo = true;
    public BatWaypointNode[] connectedNodes;

    public BatWaypointNode _batWaypointNode;
    private Button _connectButton;
    private Button _disconnectButton;
    private Button _pathfindButton;
    private Button _deleteNodesButton;

    public VisualTreeAsset VisualTree;

    private void OnEnable()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
        _batWaypointNode = (BatWaypointNode)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        //add in all the UI builder stuff 
        VisualTree.CloneTree(root);

        _connectButton = root.Q<Button>("Connect");
        _connectButton.RegisterCallback<ClickEvent>(OnConnectButtonClick);
        _disconnectButton = root.Q<Button>("Disconnect");
        _disconnectButton.RegisterCallback<ClickEvent>(OnDisconnectButtonClick);
        _pathfindButton = root.Q<Button>("Pathfind");
        _pathfindButton.RegisterCallback<ClickEvent>(OnPathfindButtonClick);
        _deleteNodesButton = root.Q<Button>("DeleteNodes");
        _deleteNodesButton.RegisterCallback<ClickEvent>(OnDeleteButtonClick);

        return root;
    }

    private void OnConnectButtonClick(ClickEvent evt) {
        if (Selection.gameObjects.Length != 2) {
            Debug.LogError("PLEASE SELECT ONLY TWO NODES");
            return;
        }

        BatWaypointNode node1Script = Selection.gameObjects[0].gameObject.GetComponent<BatWaypointNode>();
        BatWaypointNode node2Script = Selection.gameObjects[1].gameObject.GetComponent<BatWaypointNode>();
        
        if (!node1Script || !node2Script) {
            Debug.LogError("ONLY SELECT WAYPOINT NODES");
            return;
        }

        bool success = batWaypointManager.AddEdge(Selection.gameObjects[0], Selection.gameObjects[1]);
        
        if (success) {
            Debug.Log("Nodes connected!: " + node1Script.name + " " + node2Script.name);
        }
        else {
            Debug.LogWarning("FAIL: Nodes are already connected: " + node1Script.name + " " + node2Script.name);
        }
        
        SceneView.RepaintAll();
    }

    private void OnDisconnectButtonClick(ClickEvent evt) {
        if (Selection.gameObjects.Length != 2) {
            Debug.LogError("PLEASE SELECT ONLY TWO NODES");
            return;
        }

        BatWaypointNode node1Script = Selection.gameObjects[0].gameObject.GetComponent<BatWaypointNode>();
        BatWaypointNode node2Script = Selection.gameObjects[1].gameObject.GetComponent<BatWaypointNode>();
        
        if (!node1Script || !node2Script) {
            Debug.LogError("ONLY SELECT WAYPOINT NODES");
            return;
        }

        bool success = batWaypointManager.RemoveEdge(Selection.gameObjects[0], Selection.gameObjects[1]);
        if (success) {
            Debug.Log("Nodes diconnected!: " + node1Script.name + " " + node2Script.name);
        }
        else {
            Debug.LogError("FAIL: Nodes are not connected: " + node1Script.name + " " + node2Script.name);
        }
        
        SceneView.RepaintAll();
    }

    private void OnPathfindButtonClick(ClickEvent evt) {
        if (Selection.gameObjects.Length != 2) {
            Debug.LogError("PLEASE SELECT ONLY TWO NODES");
            return;
        }

        BatWaypointNode node1Script = Selection.gameObjects[0].gameObject.GetComponent<BatWaypointNode>();
        BatWaypointNode node2Script = Selection.gameObjects[1].gameObject.GetComponent<BatWaypointNode>();
        
        if (!node1Script || !node2Script) {
            Debug.LogError("ONLY SELECT WAYPOINT NODES");
            return;
        }

        GameObject[] path = batWaypointManager.BFS(Selection.gameObjects[0], Selection.gameObjects[1]);
        
        // Draw path for debugging
        for (int i = 0; i < path.Length-1; i++) {
            GameObject node = path[i];
            GameObject nextNode = path[i+1];
            Color lineColor = Color.white;
            Debug.DrawLine(node.transform.position + Vector3.up*1, nextNode.transform.position + Vector3.up*1, lineColor, 3f);
            Debug.DrawLine(node.transform.position + Vector3.down*1, nextNode.transform.position + Vector3.down*1, lineColor, 3f);
        }

        SceneView.RepaintAll();
    }

    private void OnDeleteButtonClick(ClickEvent evt) {
        GameObject[] selected = Selection.gameObjects;

        // Delete all selected nodes safely
        foreach (GameObject node in selected) {
            BatWaypointNode nodeScript = node.GetComponent<BatWaypointNode>();
            if (!nodeScript) {
                Debug.LogWarning("Could not delete " + node + " as it is not a waypoint");
                continue;
            }
            batWaypointManager.DeleteNode(node);
        }
        
        SceneView.RepaintAll();
    }
}
