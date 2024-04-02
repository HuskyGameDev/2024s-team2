using UnityEngine;

public abstract class WeaponBaseState
{
    protected WeaponsStateMachine theStateMachine;
    protected float primaryCooldown = 0.0f; // Update in the EnterState Method
    protected float secondaryCooldown = 0.0f; // Update in the EnterState Method
    protected bool attackReady = true;
    public abstract void EnterState(WeaponsStateMachine weapon); // Like Monobehavior's Start() method
    public abstract void UpdateState(WeaponsStateMachine weapon); // Like Monobehavior's Update() method
    
    protected abstract void primaryAttack();
    protected abstract void secondaryAttack();
    //protected abstract void chargeAttack();
    protected abstract void block();
    public float attackInput() // DO NOT Call this method, it is called in "WeaponsStateMachine"
    {
        if(Input.GetMouseButtonDown(0) && attackReady) {
            if(Input.GetKey(KeyCode.CapsLock)) {
                attackReady = false;
                secondaryAttack();
                return secondaryCooldown;
            }
            else {
                attackReady = false;
                primaryAttack();
                return primaryCooldown;
            }
        } else if(Input.GetMouseButton(1)) {
            block();
            return -1;
        } 
        return -1;
    } 
    public void resetAttack() {
        attackReady = true;
    }

}
