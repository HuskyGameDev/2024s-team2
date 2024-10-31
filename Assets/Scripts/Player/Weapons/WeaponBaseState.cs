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
    protected float chargeTime = 0.002f; // The time it takes to charge for the charge attack
    protected float cooldown  = 0.0f; // Set when a primary, secondary attack, or charge attack occurs
    protected bool attackReady = true;
    protected AudioSource audioSource;
    private float holdTime = 0.0f; // Tracks how long the left mouse button is held

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
        if (attackReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdTime = 0.0f; // Reset hold time when the button is first pressed
            }

            if (Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime; // Increment hold time while button is held

                if (holdTime >= chargeTime) // Charge attack
                {
                    // Charge attack
                    attackReady = false;
                    chargeAttack();
                    cooldown = chargeCooldown;
                    theStateMachine.StartWeaponCoroutine(resetAttack());
                    holdTime = 0.0f; // Reset hold time after charge attack
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (holdTime < chargeTime) // Check if the button was released before the charge time
                {
                    if (Input.GetKey(KeyCode.LeftAlt)) // Check if the left alt key is held down
                    {
                        // Secondary attack
                        attackReady = false;
                        secondaryAttack();
                        cooldown = secondaryCooldown;
                        theStateMachine.StartWeaponCoroutine(resetAttack());
                    }
                    else
                    {
                        // Primary attack
                        attackReady = false;
                        primaryAttack();
                        cooldown = primaryCooldown;
                        theStateMachine.StartWeaponCoroutine(resetAttack());
                    }
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            block();
        }
    }

    private IEnumerator resetAttack() 
    {
        yield return new WaitForSeconds(cooldown);
        attackReady = true;//currentState.resetAttack();
    }

}
