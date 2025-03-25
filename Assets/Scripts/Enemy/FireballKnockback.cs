using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireballKnockback : Destructable
{
    // Yes, this script must exist because we need something to extend enemyhealth to register attacks.
    [Header("IGNORE ALL OF THESE PROPERTIES, ABOVE AND BELOW!")]
    [SerializeField] private Fireball fireball;
    [SerializeField] private bool knockedBack = false;
    [SerializeField] private float knockbackSpeed = 30f;
    [SerializeField] GameObject playerCam;
    [SerializeField] private const string playerCamTag = "MainCamera";

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag(playerCamTag);
        fireball = gameObject.GetComponent<Fireball>();
    }

    public override void takeDamage(int damage)
    {
        if (knockedBack) return;
        Debug.Log("FIREBALL: KNOCK BACK!");
        // int damage is thrown away
        fireball.direction = playerCam.transform.forward;
        // fireball.direction = -fireball.direction;
        fireball.speed = knockbackSpeed;
        knockedBack = true;
    }
}
