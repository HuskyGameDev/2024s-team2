using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    Animator animator;
    public int damage;
    public int slamDamage;
    public AudioSource audioSource;
    public AudioClip[] attackSounds;
    public bool slam = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
