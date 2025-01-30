using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyEffect : MonoBehaviour
{

    [SerializeField] ParticleSystem bloodyEffect = null;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void Bloody()
    {
        bloodyEffect.Play();
    }
}
