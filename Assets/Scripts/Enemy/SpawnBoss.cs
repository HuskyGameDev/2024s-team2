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
    public AudioSource audioSource;
    public PlayerMovement Player;
    public GameObject bossEnemy;
    public Transform bDoor1;
    public Transform bDoor2;
    public Transform bDoor3;
    public Transform bDoor4;
    public Transform bDoor5;
    public Transform bDoor6;
    public bool bClosed = false;

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
        audioSource.clip = Player.backgroundMusic[0];
        audioSource.Play();
        bossEnemy.SetActive(true);
        PlayerPrefs.SetInt("bossKilled", 0);
    }

    public void CloseBossDoors() {
        bDoor1.transform.Rotate(0, 90, 0, Space.Self);
        bDoor2.transform.Rotate(0, 90, 0, Space.Self);
        bDoor3.transform.Rotate(0, 90, 0, Space.Self);
        bDoor4.transform.Rotate(0, 90, 0, Space.Self);
        bDoor5.transform.Rotate(0, 90, 0, Space.Self);
        bDoor6.transform.Rotate(0, 90, 0, Space.Self);
        BossSpawn();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && bClosed==false)
        {
            CloseBossDoors();
            bClosed = true;
        }
    }
}
