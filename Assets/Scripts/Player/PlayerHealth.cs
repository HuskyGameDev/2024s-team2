using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
  //public int damage = 0;
  //public string item = null;
  //public bool EnterDungeonItemUse = false; //can change depending on how the item use before the dungeon is made
  //public bool playerHit = false; //can change depending on the Player/weapon code
    public HealthBar healthBar;
    public BloodEffect bloodEffect;
    public int dam;
    public string gob = "TempGoblin_TEST(Clone)";
    public string sli_b = "Slime_Boss";
    public AudioSource audioSource;
    public AudioClip[] damageSounds;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //if statement below are for testing only
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            item = item name for the item in 1 key
            UseItem(item);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Damage(damage);
        }
        */
    }

    //public void AddHealth(int amount, bool temporary)
    public void AddHealth()
    {
        //need to put in code that remove the temporary health when getting out from dungeon...
        /*if (temporary == true)
        {
            if (currentHealth + amount <= maxHealth)
            {
                currentHealth = currentHealth + amount;
            } else
            {
                currentHealth = maxHealth;
            }
        } else
        {
            currentHealth = maxHealth;
        }*/
        if(currentHealth + PlayerPrefs.GetInt("healingVal") < maxHealth)
        {
            currentHealth = currentHealth + PlayerPrefs.GetInt("healingVal");
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
        healthBar.SetHealth(maxHealth);
    }

    public void IncMaxHP(int amount)
    {
        maxHealth = maxHealth + amount;
        currentHealth = currentHealth + amount;
        healthBar.NewMaxHealth(maxHealth, currentHealth);
    }

    public void TakeDamage(int damage, string enemy_name) //make public
    {
        float ran_dam = 0.0f;
        Debug.Log("this is for the dam" + enemy_name);
        //add armor, buff/debuff, item, and weapons? ability's code too.
        if (String.Compare(enemy_name, gob) == 0)
        {
            ran_dam = UnityEngine.Random.Range(0.8f, 1.2f);
            dam = (int)(damage * ran_dam * PlayerPrefs.GetFloat("defBuff"));
        } else if (enemy_name == "Slime_Boss")
        {
            ran_dam = UnityEngine.Random.Range(0.7f, 1.5f);
            dam = (int)(damage * ran_dam * PlayerPrefs.GetFloat("defBuff"));
        }
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
        // above line may not be right...
    }
}
