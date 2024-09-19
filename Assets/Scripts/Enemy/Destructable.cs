using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case 0:
                ingr = "A";
                PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") + num);
                amount = PlayerPrefs.GetInt("ingredientA");
                break;
            case 1:
                ingr = "B";
                PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") + num);
                amount = PlayerPrefs.GetInt("ingredientB");
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
}
