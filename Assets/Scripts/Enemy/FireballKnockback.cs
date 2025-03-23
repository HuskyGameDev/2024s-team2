using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireballKnockback : EnemyHealth
{
    // Yes, this script must exist because we need something to extend enemyhealth to register attacks.
    [Header("IGNORE ALL OF THESE PROPERTIES, ABOVE AND BELOW!")]
    [SerializeField] private Fireball fireball;

    // Start is called before the first frame update
    void Start()
    {
        fireball = gameObject.GetComponent<Fireball>();
    }

    public override void Damaging(int damage)
    {
        // int damage is thrown away
        fireball.direction = -fireball.direction;
    }
}
