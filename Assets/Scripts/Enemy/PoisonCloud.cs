using System.Collections;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public int damagePerTick = 5; 
    public float tickInterval = 1f;
    public float poisonDuration = 5f; 
    private PlayerHealth playerHealth; 
    private bool isPlayerInCloud = false;
    private Coroutine poisonCoroutine; 

    void OnTriggerEnter(Collider other)
    {
        // check if the player enters the poison cloud
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>(); 
            if (playerHealth != null && !isPlayerInCloud)
            {
                isPlayerInCloud = true;
                poisonCoroutine = StartCoroutine(ApplyPoisonDamage());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // check if the player exits the poison cloud
        if (other.CompareTag("Player") && isPlayerInCloud)
        {
            isPlayerInCloud = false;
            if (poisonCoroutine != null)
            {
                StopCoroutine(poisonCoroutine); 
            }
        }
    }

    private IEnumerator ApplyPoisonDamage()
    {
        float elapsedTime = 0f;

        while (elapsedTime < poisonDuration && isPlayerInCloud)
        {
            playerHealth.TakeDamage(damagePerTick, "PoisonCloud"); // apply damage to player
            elapsedTime += tickInterval; // wait for the next tick
            yield return new WaitForSeconds(tickInterval);
        }
    }
}