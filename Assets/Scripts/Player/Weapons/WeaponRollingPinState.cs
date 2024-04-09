using UnityEngine;


public class WeaponRollingPinState : WeaponBaseState
{
    

    private Camera playerCam;
    private float hitDistance;
    
    
    public override void EnterState(WeaponsStateMachine weapon)
    {
        Debug.Log("Player equipped with rolling pin");
        theStateMachine = weapon;
        primaryCooldown = 0.25f;
        primaryAttackDamage = 1;
        secondaryCooldown = 0.5f;
        secondaryAttackDamage = 1;
        chargeCooldown = 3f;
        chargeAttackDamage = 1;
        chargeTime = 3f;
        hitDistance = 3f;
        playerCam = Camera.main;
        audioSource = GameObject.Find("Weapon").GetComponent<AudioSource>();
    }
    public override void UpdateState(WeaponsStateMachine weapon)
    {
       base.UpdateState(weapon);
    }
   
    protected override void primaryAttack()
    {
        Debug.Log("Do primary attack");
        Debug.Log(playerCam.transform.forward);
        int layerMask = 0;
        layerMask = ~layerMask;
        RaycastHit hit;
        audioSource.Play();
        if ( Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, hitDistance, layerMask)) {
            //Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if(hit.collider.tag == "Enemy") {
                Debug.Log("An Enemy was hit");
                hit.collider.gameObject.GetComponent<Destructable>().takeDamage(primaryAttackDamage); // Deal one damage 
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
