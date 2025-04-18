using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int ene_HP;
    public int ene_dam;
    public float ran_HP;
    public string gob = "TempGoblin_TEST(Clone)";
    public string bat = "Bat";
    public string sla_b = "Slime_Boss";
    public string mino = "Minotaur";
    public Destructable destructable;
    public BloodyEffect bloodyEffect; //new
    public AudioSource audioSource;
    public AudioClip[] damageSounds;

    // Start is called before the first frame update
    void Start()
    {
        if (string.Compare(gameObject.name, gob) == 0)
        {
            ran_HP = UnityEngine.Random.Range(0.8f, 1.3f); //was .8 / 1.2
            ene_HP = (int)(10 * ran_HP);
        }
        else if (string.Compare(gameObject.name, sla_b) == 0)
        {
            ran_HP = UnityEngine.Random.Range(0.9f, 1.1f); //was .7 / 1.5
            ene_HP = (int)(50 * ran_HP);
        }
        else if (string.Compare(gameObject.name, bat) == 0)
        {
            ran_HP = UnityEngine.Random.Range(0.5f, 1.3f);
            ene_HP = (int)(8 * ran_HP); //was 10
        }
        else if (string.Compare(gameObject.name, mino) == 0)
        {
            ran_HP = UnityEngine.Random.Range(0.9f, 1.1f); //was .7 / 1.5
            ene_HP = (int)(50 * ran_HP);
        }
        else {
            ene_HP = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Damaging(int damage)
    {
        Debug.Log("DAMAGE");
        bloodyEffect.Bloody(); //new
        float ran_dam = UnityEngine.Random.Range(0.8f, 1.2f);
        ene_dam = (int)(damage * ran_dam * PlayerPrefs.GetFloat("attBuff"));
        ene_HP = ene_HP - ene_dam;
        audioSource.PlayOneShot(damageSounds[UnityEngine.Random.Range(0, damageSounds.Length - 1)]);
        if (ene_HP <= 0)
        {
            destructable.takeDamage(ene_dam);
        }
    }
}
