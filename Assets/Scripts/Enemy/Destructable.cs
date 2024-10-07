using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destructable : MonoBehaviour
{

    [SerializeField] private int health;
    private int enemyType;

    public void takeDamage(int damageDone) {
        health = health - damageDone;

        if(health <= 0) {
            DropIngredient();
            Destroy(gameObject, 0f);
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
                break;
            case 1: //slime enemy
                ingr = "slimeJelly";
                PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") + num);
                amount = PlayerPrefs.GetInt("slimeJelly");
                Notify("+" + num + " Slime Jelly");
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

    public GameObject notificationText;

    public void Notify(string message)
    {
        GameObject newNotification = Instantiate(notificationText);
        newNotification.GetComponent<RectTransform>().SetParent(GameObject.FindWithTag("NotifHolder").GetComponent<Transform>());
        newNotification.GetComponent<RectTransform>().localScale = Vector3.one;
        newNotification.GetComponent<TextMeshProUGUI>().text = message;
    }
}
