using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsStateMachine : MonoBehaviour
{
    WeaponBaseState currentState;
    public WeaponRollingPinState RollingPinState = new WeaponRollingPinState();
    public WeaponKnifeState KnifeState = new WeaponKnifeState();
    public WeaponCleaverState CleaverState = new WeaponCleaverState();

    public Image img;
    public GameObject rollingPin;
    public GameObject cleaver;
    public GameObject rollingPinUI;
    public GameObject cleaverUI;

    public AudioClip[] rollingPinClips;
    public AudioClip[] knifeClips;
    public int myInt = 100;


    // Start is called before the first frame update
    void Start()
    {
        // Equip the player with the weapon of choice
        //currentState = RollingPinState; // TODO This will eventually be set to whichever weapon the player selected before beginning the level
        rollingPin.SetActive(false);
        cleaver.SetActive(false);
        rollingPinUI.SetActive(false);
        cleaverUI.SetActive(false);

        switch (PlayerPrefs.GetInt("equippedWeapon")){
            case 1:
                currentState = RollingPinState;
                rollingPin.SetActive(true);
                rollingPinUI.SetActive(true);
                break;
            case 2://change to cleaver once ready
                currentState = CleaverState;
                cleaver.SetActive(true);
                cleaverUI.SetActive(true);
                break;
            case 3:
                currentState = RollingPinState;
                break;
            case 4:
                currentState = RollingPinState;
                break;
            default:
                currentState = RollingPinState;
                rollingPin.SetActive(true);
                break;
        }
        
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
