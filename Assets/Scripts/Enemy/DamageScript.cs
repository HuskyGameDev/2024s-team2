using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScrip : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public int damage = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            /*
            var clas = new PlayerHealth();
            int damage = 10;
            clas.TakeDamage(damage);
            */
            PlayerHealth.TakeDamage(damage);
        }
    }
}
