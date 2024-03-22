using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsStateMachine : MonoBehaviour
{
    WeaponBaseState currentState;
    public WeaponRollingPinState RollingPinState = new WeaponRollingPinState();
    public WeaponKnifeState KnifeState = new WeaponKnifeState();
    private float cooldown;


    // Start is called before the first frame update
    void Start()
    {
        // Equip the player with the weapon of choice
        currentState = RollingPinState; // TODO This will eventually be set to whichever weapon the player selected before beginning the level

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        cooldown = currentState.attackInput();
        
        if(cooldown >= 0) {
            StartCoroutine(resetAttack());
        }
    }

    private IEnumerator resetAttack() 
    {
        yield return new WaitForSeconds(cooldown);
        currentState.resetAttack();
    }
}
