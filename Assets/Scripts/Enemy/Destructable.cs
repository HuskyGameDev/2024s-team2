using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    [SerializeField] private int health;

    public void takeDamage(int damageDone) {
        health = health - damageDone;

        if(health <= 0) {
            Destroy(gameObject, 0f);
        }
    }
}
