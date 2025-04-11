using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public Animator animator;
    public int damage;
    public int slamDamage;
    public AudioSource audioSource;
    public AudioClip[] attackSounds;
    public bool slam = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, gameObject.name); // Apply damage to the player
                Debug.Log($"Player hit by {gameObject.name}, damage dealt: {damage}");
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on player.");
            }
        }
        else
        {
            Debug.LogWarning("Player object not found.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            damage = 10;
            if (slam)
            {
                damage = slamDamage;
                Debug.Log("Player hit with slam!");
            }
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject.name);
            if (slam == false)
            {
                animator.SetTrigger("Attack");
                audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length - 1)]);
            }
            slam = false;
            // WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        }
        //slam = false;
    }
}
