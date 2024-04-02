using UnityEngine;
using System.Collections;

public abstract class WeaponBaseState
{
    protected WeaponsStateMachine theStateMachine;
    protected float primaryCooldown = 0.0f; // Update in the EnterState Method
    protected float secondaryCooldown = 0.0f; // Update in the EnterState Method
    
    protected float cooldown  = 0.0f; // Set when a primary or secondary attack occurs
    protected bool attackReady = true;
    public abstract void EnterState(WeaponsStateMachine weapon); // Like Monobehavior's Start() method
    public virtual void UpdateState(WeaponsStateMachine weapon) // Like Monobehavior's Update() method
    {
        attackInput();
    }
    protected abstract void primaryAttack();
    protected abstract void secondaryAttack();
    //protected abstract void chargeAttack();
    protected abstract void block();
    protected void attackInput() // 
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
