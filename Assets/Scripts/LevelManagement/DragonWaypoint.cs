using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWaypoint : MonoBehaviour
{
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.7f);
    }
}
