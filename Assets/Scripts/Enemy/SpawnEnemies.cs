using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    //spawn enemies for level 1, have a total number of enemies that will spawn for the level, and a max that can be alive at once
    //spawn enemies in as other enemies die to reach the max, until the max can no longer be reached
    //ex: max 60 enemies for level 1, only 15 can be spawned in at once, if 1 dies spawn 1 more enemy so the number alive is still 15

    public GameObject enemy;
    public int xpos;
    public int zpos;
    public TextMeshProUGUI numEnemiesLeft;
    public int level;
    public GameObject endLevel;
    private int enemyAmount = 7; 
    // private int enemyAmount = 0; //For testing the slime

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("enemyCount", 0);
        PlayerPrefs.SetInt("enemiesLeft", 20);
        PlayerPrefs.SetInt("enemiesNotKilled", 20);
        Debug.Log("starting");
        StartCoroutine(SpawnEnemy());
        PlayerPrefs.SetInt("bossKilled", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("enemies: " + PlayerPrefs.GetInt("enemyCount"));
        //Debug.Log("enemies left: " + PlayerPrefs.GetInt("enemiesLeft"));
        if(PlayerPrefs.GetInt("enemiesNotKilled") > 0)
        {
            numEnemiesLeft.text = "Enemies Left: " + PlayerPrefs.GetInt("enemiesNotKilled");
            endLevel.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("bossKilled") == 0)
        {
            numEnemiesLeft.text = "A Boss has spawned!";
            endLevel.SetActive(false);
        }
        else
        {
            numEnemiesLeft.text = "Boss defeated!";
            endLevel.SetActive(true);
            if (level == 1)
            {
                PlayerPrefs.SetInt("haveCleaver",1);
            }
            else if(level == 2)
            {
                PlayerPrefs.SetInt("haveWeapon2", 1);
            }
            else
            {
                PlayerPrefs.SetInt("haveWeapon3", 1);
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while(PlayerPrefs.GetInt("enemiesLeft") > 0)
        {
            for(int i = 0; i < enemyAmount; i++)
            {
                if (PlayerPrefs.GetInt("enemyCount") < enemyAmount)
                {
                    Debug.Log("enemies < " + enemyAmount);
                    if (Random.Range(0, 100) > 50)
                    { 
                        xpos = Random.Range(-31, -24);
                        zpos = Random.Range(-31, 3);
                    }
                    else
                    {
                        xpos = Random.Range(32, 35);
                        zpos = Random.Range(-31, 3);
                    }
                    if ((PlayerPrefs.GetInt("enemyCount") < enemyAmount) && (PlayerPrefs.GetInt("enemiesLeft") > 0))
                    {
                        GameObject newEnemy = Instantiate(enemy, new Vector3(xpos, 0, zpos), Quaternion.identity);
                        PlayerPrefs.SetInt("enemyCount", PlayerPrefs.GetInt("enemyCount") + 1);
                        PlayerPrefs.SetInt("enemiesLeft", PlayerPrefs.GetInt("enemiesLeft") - 1);
                        Debug.Log("spawned");
                        yield return new WaitForSeconds(1);
                        Debug.Log("waited");
                    }
                }
            }
            yield return new WaitForSeconds(3);
        }
        Debug.Log("end spawn enemy");
    }
}
