using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [Header("Circling/Waypoints")]
    [SerializeField] private List<GameObject> waypoints;
    [SerializeField] private List<GameObject> currentPath;
    [SerializeField] public float waypointReachedDistance = 0.1f;
    [SerializeField] private float circleSpeed;

    [Header("Debugging")]
    [SerializeField] public bool debugPrint;

    [Header("Combat")]
    [SerializeField] private float attackTimer;
    [SerializeField] private Vector2 timeBetweenAttacks;
    [SerializeField] private int swoopDamage;
    [SerializeField] private GameObject fireballPrefab;

    [Header("Movement")]
    [SerializeField] private float rotationSpeed;

    [Header("States")]
    [SerializeField] private bool lethal;    
    [SerializeField] private enum DragonState {CIRCLING, SHOOTING_FIREBALL, SWOOPING};
    [SerializeField] private DragonState state = DragonState.CIRCLING;

    [Header("Misc")]
    [SerializeField] private const string cameraTag = "MainCamera";
    [SerializeField] private const string playerTag = "Player";
    [SerializeField] private Transform player;

    [Header("Swoop")]
    [SerializeField] private float swoopSpeed;
    [SerializeField] private Vector3 startSwoopPos;
    [SerializeField] private Vector3 endSwoopPos;
    [SerializeField] private Vector3 midSwoopPos;
    [SerializeField] private AnimationCurve swoopVertCurve;
    [SerializeField] private AnimationCurve swoopHorzCurve;
    [SerializeField] private float swoopDepthCorrection; // Bezier curve correction.
    [SerializeField] private float swoopParameter;
    [SerializeField] private float swoopRangeRadius;

    [Header("Effects")]
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] roarSounds;
    private CameraEffects cameraEffects;
    private AudioSource audioSource;
    

    private void Roar() {
        cameraEffects.ActivateFOV(1.2f, 2, 1);
        cameraEffects.ActivateShake(60, 2);
        // TODO: Play sound
        audioSource.PlayOneShot(roarSounds[Random.Range(0, roarSounds.Length)]);
    }

    // Keeps our dragon aligned with a target while not allowing for instantaneous, unnatural turns
    private void FaceTarget(Vector3 targetPos) {
        Vector3 dir = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        if (dir.sqrMagnitude < 0.0001f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private GameObject RandomWaypoint() {
        int randomIndex = Random.Range(0, waypoints.Count);
        return waypoints[randomIndex];
    }

    private void CircleState() {
        GameObject currentTarget = currentPath[0];
        if (currentTarget == null || Vector3.Distance(currentTarget.transform.position, transform.position) < waypointReachedDistance) {
            currentPath = new List<GameObject>(1) {RandomWaypoint()};
            currentTarget = currentPath[0];
        }
        
        // transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, circleSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position+transform.forward, circleSpeed * Time.deltaTime);
        FaceTarget(currentTarget.transform.position);
    }

    private void StartSwoop() {
        Vector3 dir = player.position - transform.position;
        startSwoopPos = transform.position;
        endSwoopPos = Vector3.right * 2*dir.x + Vector3.forward * 2*dir.z + transform.position;
        midSwoopPos = new Vector3(player.position.x, player.position.y + dir.y * (swoopDepthCorrection-1), player.position.z);
        state = DragonState.SWOOPING;
    } 
    private void ShootFireball() {
        Vector3 dir = player.position - transform.position;
        GameObject fireballInstance = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Fireball fireball = fireballInstance.GetComponent<Fireball>();
        fireball.direction = dir.normalized;
        Debug.DrawLine(player.position, transform.position, Color.red, 7f);
    }

    private void MoveTowardsPlayer() {
        Vector3 dir = player.position - transform.position;
        Vector3 moveTarget = Vector3.right * 2*dir.x + Vector3.forward * 2*dir.z + (player.transform.position + Vector3.up*20); // Changed to avoid travelling up
        // Vector3 moveTarget = Vector3.right * 2*dir.x + Vector3.forward * 2*dir.z + transform.position;
        // transform.position = Vector3.MoveTowards(transform.position, moveTarget, circleSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position+transform.forward, circleSpeed * Time.deltaTime); // Just travel forward for more natural movement
        FaceTarget(moveTarget);
    }

    private void SwoopState() {
        // Move closer if we aren't in range
        if (Vector3.Distance(transform.position, player.position) > swoopRangeRadius && !lethal) {
            Vector3 dir = player.position - transform.position;
            Debug.DrawLine(transform.position, transform.position + (dir.normalized * swoopRangeRadius), Color.blue);
            MoveTowardsPlayer();
            return;
        }
        if (!lethal) {
            StartSwoop();
        }

        lethal = true;
        Debug.DrawLine(startSwoopPos, midSwoopPos, Color.red);
        Debug.DrawLine(midSwoopPos, endSwoopPos, Color.red);

        // Debug.Log(Vector3.Distance(startSwoopPos, endSwoopPos));
        swoopParameter += swoopSpeed * Time.deltaTime * (46/Vector3.Distance(startSwoopPos, endSwoopPos)); // Speed correction for distance

        transform.position = (Vector3.Lerp(startSwoopPos, endSwoopPos, swoopHorzCurve.Evaluate(swoopParameter)) + Vector3.Lerp(transform.position, midSwoopPos, swoopVertCurve.Evaluate(swoopParameter)) )/2;
        FaceTarget((Vector3.Lerp(startSwoopPos, endSwoopPos, swoopHorzCurve.Evaluate(swoopParameter+0.001f)) + Vector3.Lerp(transform.position, midSwoopPos, swoopVertCurve.Evaluate(swoopParameter+0.001f)) )/2);
        if (swoopParameter >= 1f) {
            lethal = false;
            state = DragonState.CIRCLING;
            swoopParameter = 0f;
        }
    }

    private void ChooseRandomAttack() {
        int randomAttackIndex = Random.Range(0, 2);
        switch (randomAttackIndex) {
            case 0:
                StartSwoop();
                break;
            case 1:
                ShootFireball();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("DragonWaypoint").ToList<GameObject>();
        currentPath = new List<GameObject>(1) {RandomWaypoint()};
        player = GameObject.FindWithTag(playerTag).transform;
        attackTimer = Random.Range(timeBetweenAttacks.x, timeBetweenAttacks.y);
        audioSource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        cameraEffects = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<CameraEffects>();
        Roar();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && Vector3.Distance(transform.position, player.position) < 100f) {
            Roar();
            ChooseRandomAttack();
            attackTimer = Random.Range(timeBetweenAttacks.x, timeBetweenAttacks.y);
        }
        if (state == DragonState.CIRCLING)
        {
            CircleState();
        }
        else if (state == DragonState.SWOOPING) {
            SwoopState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter dragon");
        if (other.gameObject.CompareTag(playerTag) && lethal)
        {
            Debug.Log("Hit player dragon");
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(swoopDamage, gameObject.name);
            // animator.SetTrigger("Attack");
            // audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length - 1)]);
            lethal = false;
        }
    }
}
