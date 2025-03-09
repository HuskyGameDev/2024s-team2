using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BatAI : MonoBehaviour
{
    public BatWaypointManager batWaypointManager;
    private List<GameObject> batWaypointNodes;

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

        GameObject[] path = batWaypointManager.BFS(batWaypointManager.GetClosestNode(transform.position), batWaypointNodes[randomWaypointIndex]);
        for (int i = 0; i < path.Length - 1; i++) {
            Transform nextNodeTransform;
            nextNodeTransform = path[i].transform;
            // We need to change the x and y rotation of the bat to match the lookvec
            Vector3 lookVec = (nextNodeTransform.position - transform.position).normalized;
            while (Vector3.Distance(nextNodeTransform.position, transform.position) > 1) {
                Quaternion lookRotation = Quaternion.LookRotation(lookVec);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
                transform.position = Vector3.MoveTowards(transform.position, nextNodeTransform.position, speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("BAT: Reached WP!");
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
