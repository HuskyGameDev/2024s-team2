using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // [SerializeField] ParticleSystem fireParticles;
    [SerializeField] GameObject impactParticles;

    public float speed;
    public int damage;
    public Vector3 direction;
    public float explosionRadius;

    private float lifeTime = 0f;

    // private string enemyTag = "Enemy";
    [SerializeField] private const string playerTag = "Player";

    public Fireball(Vector3 direction) {
        this.direction = direction;
        this.speed = 10f;
        this.damage = 40;
        this.lifeTime = 0f;
    }
    void Start()
    {
        direction = direction.normalized;
        impactParticles = gameObject.transform.GetChild(1).gameObject;
        // fireParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position+direction, speed*Time.deltaTime);
        if (lifeTime > 40f) {
            Explode();
        }
    }

    private void Explode() {
        // Calculate damage
        GameObject player = GameObject.FindWithTag(playerTag);
        Vector3 playerPos = player.transform.position;
        float distToPlayer = Vector3.Distance(playerPos, transform.position);
        if (distToPlayer <= explosionRadius) {
            int damageDealt = Mathf.RoundToInt(damage * Mathf.Clamp01((explosionRadius - distToPlayer) / explosionRadius));
            player.GetComponent<PlayerHealth>().TakeDamage(damageDealt, gameObject.name);
        }
        
        impactParticles.transform.parent = null;
        impactParticles.GetComponent<ParticleSystem>().Play();
        Destroy(impactParticles, impactParticles.GetComponent<ParticleSystem>().main.duration);

        // TODO: Sound here

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision!");
        if (lifeTime < 0.5f) {
            return;
        }
        Debug.Log("Fireball explosion!");

        Explode();
    }
}
