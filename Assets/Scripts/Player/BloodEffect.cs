using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{

    [SerializeField] ParticleSystem bloodEffect = null;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void Blood()
    {
        bloodEffect.Play();
    }
}
