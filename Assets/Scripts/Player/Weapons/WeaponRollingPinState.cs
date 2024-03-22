using UnityEngine;


public class WeaponRollingPinState : WeaponBaseState
{
    
    public override void EnterState(WeaponsStateMachine weapon)
    {
        Debug.Log("Player equipped with rolling pin");
        primaryCooldown = 5f;
        secondaryCooldown = 10f;
    }
    public override void UpdateState(WeaponsStateMachine weapon)
    {
       
    }
   
    protected override void primaryAttack()
    {
        Debug.Log("Do primary attack");
    }

    protected override void secondaryAttack() 
    {
        Debug.Log("Do secondary attack");
    }
    //protected override void chargeAttack();
    protected override void block() 
    {
        Debug.Log("Do Block");
    }
}
