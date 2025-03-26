using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWaypoint : MonoBehaviour
{
    // Basic file for drawing gizmos in the editor or during gameplay
    public float radius;

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
