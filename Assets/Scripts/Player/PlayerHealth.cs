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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //if statement below are for testing only
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //item = item name for the item in 1 key
          //UseItem(item);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(10);
           //Damage(damage);
        //}
    }

    public void AddHealth(int amount, bool temporary)
    {
        if (temporary == true)
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
        }
    }

    public void TakeDamage(int damage) //make public
    {
        //add armor, buff/debuff, item, and weapons? ability's code too.
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene("MainMenu");
        // above line may not be right...
    }
}
