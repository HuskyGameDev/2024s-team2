using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;
    public Transform targetNodeTransform;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        batWaypointManager = GameObject.FindGameObjectWithTag("BatWaypointManager").GetComponent<BatWaypointManager>();
        batWaypointNodes = batWaypointManager.batWaypointNodes;
        StartCoroutine(FiniteStateMachine());
        Debug.Log("BAT ONLINE!!!!");
    }

    IEnumerator FiniteStateMachine() {
        while (!batWaypointManager.isInstantiated)
            yield return null;
        batWaypointNodes = batWaypointManager.batWaypointNodes;
        while (true) {
            Debug.Log("BAT: Patrolling...");
            yield return StartCoroutine(patrollingState());;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator patrollingState() {
        int randomWaypointIndex = UnityEngine.Random.Range(0, batWaypointNodes.Count);
        Debug.Log("BAT: Moving to wp" + randomWaypointIndex);
        targetNodeTransform = batWaypointNodes[randomWaypointIndex].transform;
        Debug.Log("BAT: targetNodePos: " + targetNodeTransform.position);
        Debug.Log("BAT: targetNode instance: " + targetNodeTransform.name);
        while (Vector3.Distance(targetNodeTransform.position, transform.position) > 1) {
            transform.position = Vector3.MoveTowards(transform.position, targetNodeTransform.position, speed*Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("BAT: Reached WP!");
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
