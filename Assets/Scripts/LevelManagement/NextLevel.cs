using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 180, 0);

    void Update()
    {
        transform.LookAt(player);
        transform.Rotate(offset);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("CraftMenu");
        }
    }
}
