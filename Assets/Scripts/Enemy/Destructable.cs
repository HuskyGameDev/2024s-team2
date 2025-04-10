using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destructable : MonoBehaviour
{

    public int enemyType;
    public AudioSource audioSource;
    public AudioClip[] damageSounds;
    public ParticleSystem bloodyEffect;

    public void takeDamage(int damageDone) {
        audioSource.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length - 1)]);
        DropIngredient();
        AudioSource.PlayClipAtPoint(damageSounds[Random.Range(0, damageSounds.Length - 1)], gameObject.transform.position);
        ParticleSystem deadeffect = Instantiate(bloodyEffect)
                                       as ParticleSystem;
        deadeffect.transform.position = transform.position;

        //play it
        deadeffect.loop = false;
        deadeffect.Play();

        //destroy the particle system when its duration is up, right
        //it would play a second time.
        Destroy(deadeffect.gameObject, deadeffect.duration);

        Destroy(gameObject, 0f);
        if(enemyType == 2)
        {
            PlayerPrefs.SetInt("bossKilled", 1);
        }
    }

    public void DropIngredient()
    {
        int num = Random.Range(1, 3);
        string ingr;
        int amount;
        switch (enemyType)
        {
            case 0: //goblin enemy
                ingr = "goblinMeat";
                PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") + num);
                amount = PlayerPrefs.GetInt("goblinMeat");
                Notify("+" + num + " Goblin Meat");
                PlayerPrefs.SetInt("found1", 1);
                break;
            case 1: //slime enemy
                ingr = "slimeJelly";
                PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") + num);
                amount = PlayerPrefs.GetInt("slimeJelly");
                Notify("+" + num + " Slime Jelly");
                PlayerPrefs.SetInt("found2", 1);
                break;
            case 2: //slime boss
                int bonus = Random.Range(5, 10);
                ingr = "slimeJelly";
                PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") + num + bonus);
                PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") + 1);
                amount = PlayerPrefs.GetInt("slimeJelly");
                PlayerPrefs.SetInt("haveCleaver", 1);
                PlayerPrefs.SetInt("found2", 1);
                PlayerPrefs.SetInt("found3", 1);
                enemyType = 1;
                Notify("+" + (num + bonus) + " Slime Jelly");
                enemyType = 2;
                Notify("+" + 1 + " Slime Boss Jelly");
                break;
            case 3: //bat enemy
                ingr = "bloodBerry";
                PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") + num);
                amount = PlayerPrefs.GetInt("bloodBerry");
                Notify("+" + num + " Blood Berry");
                PlayerPrefs.SetInt("found6", 1);
                break;
            case 4: //minotaur boss
                ingr = "horn";
                PlayerPrefs.SetInt("horn", PlayerPrefs.GetInt("horn") + num);
                amount = PlayerPrefs.GetInt("horn");
                Notify("+" + num + " Horn");
                PlayerPrefs.SetInt("found4", 1);
                break;
            case 5: //mushrooom enemy
                ingr = "mushroom";
                PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") + num);
                amount = PlayerPrefs.GetInt("mushroom");
                Notify("+" + num + " Mushroom");
                PlayerPrefs.SetInt("found5", 1);
                break;
            case 6: //skeleton enemy
                ingr = "bone";
                PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") + num);
                amount = PlayerPrefs.GetInt("bone");
                Notify("+" + num + " Bone");
                PlayerPrefs.SetInt("found7", 1);
                break;
            default:
                ingr = "A";
                PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") + num);
                amount = PlayerPrefs.GetInt("ingredientA");
                break;
        }
        Debug.Log("ingredient " + ingr + " : " + amount + " amount added: " + num);
        PlayerPrefs.SetInt("enemyCount", PlayerPrefs.GetInt("enemyCount") - 1);
        PlayerPrefs.SetInt("enemiesNotKilled", PlayerPrefs.GetInt("enemiesNotKilled") - 1);
        //Debug.Log("KILLED");
    }

    public GameObject[] notificationText;

    public void Notify(string message)
    {
        GameObject newNotification = Instantiate(notificationText[enemyType]);
        newNotification.GetComponent<RectTransform>().SetParent(GameObject.FindWithTag("NotifHolder").GetComponent<Transform>());
        newNotification.GetComponent<RectTransform>().localScale = Vector3.one;
        newNotification.GetComponent<TextMeshProUGUI>().text = message;
    }
}
