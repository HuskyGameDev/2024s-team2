using UnityEngine;


public class WeaponRollingPinState : WeaponBaseState
{

    public Animator animator;
    private Camera playerCam;
    private float hitDistance;
    private WeaponsStateMachine machineScript;
    
    
    public override void EnterState(WeaponsStateMachine weapon)
    {
        Debug.Log("Player equipped with rolling pin");
        theStateMachine = weapon;
        primaryCooldown = 1f;
        primaryAttackDamage = 7;
        secondaryCooldown = 0.5f;
        secondaryAttackDamage = 1;
        chargeCooldown = 3f;
        chargeAttackDamage = 1;
        chargeTime = 3f;
        hitDistance = 3f;
        playerCam = Camera.main;
        audioSource = GameObject.Find("Weapon").GetComponent<AudioSource>();
        machineScript = GameObject.Find("Weapon").GetComponent<WeaponsStateMachine>();
        animator = GameObject.Find("Weapon").GetComponent<Animator>();
    }
    public override void UpdateState(WeaponsStateMachine weapon)
    {
       base.UpdateState(weapon);
    }
   
    protected override void primaryAttack()
    {
        Debug.Log("Do primary attack");
        Debug.Log(playerCam.transform.forward);
        animator.SetTrigger("Active");
        int layerMask = 0;
        layerMask = ~layerMask;
        RaycastHit hit;
        //audioSource.Play();
        audioSource.PlayOneShot(machineScript.rollingPinClips[Random.Range(0, machineScript.rollingPinClips.Length - 1)]);
        if ( Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, hitDistance, layerMask)) {
            //Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if(hit.collider.tag == "Enemy") {
                Debug.Log("An Enemy was hit");
                hit.collider.gameObject.GetComponent<EnemyHealth>().Damaging(primaryAttackDamage); // Deal one damage 
            } else {
                Debug.Log("An Enemy was not hit");
            }
        }
        else
        {
            //Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    protected override void secondaryAttack() 
    {
        Debug.Log("Do secondary attack");
    }
    protected override void chargeAttack()
    {
        Debug.Log("Do secondary attack");
    }
    protected override void block() 
    {
        Debug.Log("Do Block");
    }
}
