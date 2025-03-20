using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{

    private PlayerHealth playerHealth;
    private float waitTime = 3f;
    private float nextDamage = 0;
    private int damage = 40;
    private string lava = "Lava";
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (Time.time > nextDamage && other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, lava);
            nextDamage = Time.time + waitTime;
        }
    }
}
