using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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

        batWaypointManager.BFS(Selection.gameObjects[0], Selection.gameObjects[1]);
        
        SceneView.RepaintAll();
    }
}
