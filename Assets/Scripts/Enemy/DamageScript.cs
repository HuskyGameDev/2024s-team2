using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    Animator animator;
    public int damage;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damage = 10;
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject.name);
            animator.SetTrigger("Attack");
           // WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        }

    }
}
