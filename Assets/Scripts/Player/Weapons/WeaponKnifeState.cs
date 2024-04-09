using UnityEngine;

public class WeaponKnifeState : WeaponBaseState
{
    public override void EnterState(WeaponsStateMachine weapon)
    {
        Debug.Log("Player equipped with the Knife");

    }
    public override void UpdateState(WeaponsStateMachine weapon)
    {
        
    }
  
    protected override void primaryAttack()
    {

    }

    protected override void secondaryAttack()
    {

    }
    protected override void chargeAttack()
    {
        
    }
    protected override void block()
    {

    }

}
