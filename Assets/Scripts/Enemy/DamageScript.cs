using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    Animator animator;
    public int damage = 10;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            animator.SetTrigger("Attack");
           // WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        }

    }
}
