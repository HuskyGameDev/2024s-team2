using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //spawn enemies for level 1, have a total number of enemies that will spawn for the level, and a max that can be alive at once
    //spawn enemies in as other enemies die to reach the max, until the max can no longer be reached
    //ex: max 60 enemies for level 1, only 15 can be spawned in at once, if 1 dies spawn 1 more enemy so the number alive is still 15

    public GameObject enemy;
    public int xpos;
    public int zpos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("enemyCount", 0);
        PlayerPrefs.SetInt("enemiesLeft", 20);
        Debug.Log("starting");
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        Debug.Log("spawnenemy function");
        //while(PlayerPrefs.GetInt("enemiesLeft") > 0)
       // {

            while (PlayerPrefs.GetInt("enemyCount") < 10)
            {
                Debug.Log("enemies < 10");
                xpos = Random.Range(-5, 5);
                zpos = Random.Range(4, 4);
                GameObject newEnemy = Instantiate(enemy, new Vector3(xpos, 0, zpos), Quaternion.identity);
                PlayerPrefs.SetInt("enemyCount", PlayerPrefs.GetInt("enemyCount") + 1);
                Debug.Log("spawned");
                yield return new WaitForSeconds(1);
                Debug.Log("waited");
            }
       // }
    }
}
