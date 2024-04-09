using UnityEngine;
using System.Collections;

public abstract class WeaponBaseState
{
    protected WeaponsStateMachine theStateMachine;
    protected GameObject player;
    protected float primaryCooldown = 0.0f; // Update in the EnterState Method
    protected int primaryAttackDamage = 0; // Update in the EnterState Method
    protected float secondaryCooldown = 0.0f; // Update in the EnterState Method
    protected int secondaryAttackDamage = 0; // Update in the EnterState Method
    protected float chargeCooldown = 0.0f; // Update in the EnterState Method
    protected int chargeAttackDamage = 0; // Update in the EnterState Method
    protected float chargeTime = 0.0f; // The time it takes to charge for the charge attack
    protected float cooldown  = 0.0f; // Set when a primary, secondary attack, or charge attack occurs
    protected bool attackReady = true;
    protected AudioSource audioSource;
    
    public abstract void EnterState(WeaponsStateMachine weapon); // Like Monobehavior's Start() method
    public virtual void UpdateState(WeaponsStateMachine weapon) // Like Monobehavior's Update() method
    {
        attackInput();
    }
    protected abstract void primaryAttack();
    protected abstract void secondaryAttack();
    protected abstract void chargeAttack();
    protected abstract void block();
    protected void attackInput() 
    {
        if(Input.GetMouseButtonDown(0) && attackReady) {
            if(Input.GetKey(KeyCode.CapsLock)) {
                attackReady = false;
                secondaryAttack();
                cooldown = secondaryCooldown;
                theStateMachine.StartWeaponCoroutine(resetAttack());
            }
            else {
                attackReady = false;
                primaryAttack();
                cooldown = primaryCooldown;
                theStateMachine.StartWeaponCoroutine(resetAttack());
            }
        } else if(Input.GetMouseButton(1)) {
            block();
        } 
    } 
   
    private IEnumerator resetAttack() 
    {
        yield return new WaitForSeconds(cooldown);
        attackReady = true;//currentState.resetAttack();
    }

}
