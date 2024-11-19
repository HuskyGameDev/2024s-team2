using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    Animator animator;
    public int damage;
    public AudioSource audioSource;
    public AudioClip[] attackSounds;

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
            audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length - 1)]);
            // WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        }

    }
}
