using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public BloodEffect bloodEffect;
    public int dam;
    public string gob = "TempGoblin_TEST(Clone)";
    public string bat = "Bat";
    public string sli_b = "Slime_Boss";
    public AudioSource audioSource;
    public AudioClip[] damageSounds;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    

    public void AddHealth()
    {
        if (currentHealth + PlayerPrefs.GetInt("healingVal") < maxHealth)
        {
            currentHealth += PlayerPrefs.GetInt("healingVal");
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void FullRecover()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    public void IncMaxHP(int amount)
    {
        maxHealth = maxHealth + amount;
        currentHealth = currentHealth + amount;
        healthBar.NewMaxHealth(maxHealth, currentHealth);
    }

    public void TakeDamage(int damage, string enemy_name)
    {
        float ran_dam = 0.0f;
        Debug.Log("damage from source: " + enemy_name);

        ran_dam = UnityEngine.Random.Range(0.8f, 1.2f);
        dam = (int)(damage * ran_dam * PlayerPrefs.GetFloat("defBuff"));

        currentHealth -= dam;
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(damageSounds[UnityEngine.Random.Range(0, damageSounds.Length - 1)]);
        bloodEffect.Blood();
        if (currentHealth < 1)
        {
            KillPlayer();
        }
    }


    void KillPlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }
}