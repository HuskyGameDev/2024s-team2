using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsStateMachine : MonoBehaviour
{
    WeaponBaseState currentState;
    public WeaponRollingPinState RollingPinState = new WeaponRollingPinState();
    public WeaponKnifeState KnifeState = new WeaponKnifeState();


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
    }

    // Use this method rather than "StartCoroutine()" from the 
    // weapon states because they do not inherate from Monobehavior.
    // You can still put your IEnumerator in the Weapon state file
    // as long as you import from "System.Collections". See "WeaponBaseState.cs"
    // for examples on how to use coroutines in the weapon states.
    public void StartWeaponCoroutine(IEnumerator routine) {
        StartCoroutine(routine);
    }

    
}
