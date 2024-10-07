using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnBoss : MonoBehaviour
{
    private bool bossNotSpawned = true;
    public Transform door1;
    public Transform door2;
    public Transform door3;
    public Transform door4;
    public Transform door5;
    public Transform door6;
    public TextMeshProUGUI numEnemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("enemiesNotKilled") == 0 && bossNotSpawned)
        {
            bossNotSpawned = false;
            OpenDoors();
            
        }
    }

    public void OpenDoors()
    {
        door1.transform.Rotate(0, 90, 0, Space.Self);
        door2.transform.Rotate(0, 90, 0, Space.Self);
        door3.transform.Rotate(0, 90, 0, Space.Self);
        door4.transform.Rotate(0, 90, 0, Space.Self);
        door5.transform.Rotate(0, 90, 0, Space.Self);
        door6.transform.Rotate(0, 90, 0, Space.Self);
    }

    public void BossSpawn()
    {
        
    }
}
