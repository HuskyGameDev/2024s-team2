using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public int health;
    public int enemyType;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TakingDamage());
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);
    }

    void Die()
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
        //PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA")+num);
        Debug.Log("ingredient " + ingr +" : " + amount + " amount added: " + num);
        Destroy(gameObject, 0f);
    }

    IEnumerator TakingDamage()
    {
        while (health > 0)
        {
            yield return new WaitForSecondsRealtime(5);
            health -= 5;
        }
        Die();
    }
}
