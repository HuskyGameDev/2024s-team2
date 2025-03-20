using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFire : MonoBehaviour
{

    [SerializeField] ParticleSystem lavaFire = null;
    private bool OnOff = false;
    private float waitTime = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", waitTime, waitTime);
    }

    /**
    void update()
    {
        
    }
    **/

    // Update is called once per frame
    public void Fire()
    {
        if (OnOff == true)
        {
            lavaFire.Stop();
            OnOff = false;
        } else
        {
            lavaFire.Play();
            OnOff = true;
        }
    }
}
