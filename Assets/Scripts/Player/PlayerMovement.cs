using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchYScale;
    private float startYScale;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float gravityMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    private float playerHeight;
    [SerializeField] private LayerMask Ground;
    bool grounded;

    // [Header("Slope Handling")]
    // [SerializeField] private float maxSlopeAngle;
    // private RaycastHit slopeHit;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private AudioClip[] walkingClips;
    [SerializeField] private float stepSpeed;
    [SerializeField] private AudioClip[] jumpingClips;
    [SerializeField] public AudioClip[] backgroundMusic;
    private float footstepTimer;

    [Header("Other")]
    public int levelNum;

    private Transform orientation;
    private Transform weapon;
    float horizontalInput;
    float verticleInput;

    Vector3 moveDirection;

    Rigidbody rb;
    
    public MovementState state;
    public enum MovementState {walking, sprinting, crouching, air}

    // Public variable for enabling/disabling jumping
    public bool jumpEnabled = true; 

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("lastLevel", levelNum);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        footstepTimer = 0;
        startYScale = transform.localScale.y;
        orientation = transform.Find("PlayerBody");
        playerHeight = orientation.localScale.y;
        weapon = GameObject.FindGameObjectWithTag("Weapon").transform;
        
        // Simulate one crouch. Lazy fix!
        weapon.localPosition = new Vector3(weapon.localPosition.x, crouchYScale - startYScale*0.9f, weapon.localPosition.z);
        weapon.localPosition = new Vector3(weapon.localPosition.x, weapon.localPosition.y + startYScale - crouchYScale, weapon.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        walkSpeed = PlayerPrefs.GetInt("speedBoostWalk");
        sprintSpeed = PlayerPrefs.GetInt("speedBoostSprint");

        // Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.6f, Ground); 

        MyInput();
        SpeedControl();
        StateHandler();

        // Apply drag
        if(grounded)
            rb.drag = groundDrag;
        else   
            rb.drag = 0f;
    }

    void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticleInput = Input.GetAxisRaw("Vertical");

        // Jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded && jumpEnabled) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // Wait for the jump to cooldown before allowing a second jump
        }

        // Crouch
        if(Input.GetKeyDown(crouchKey)) {
            Vector3 startScale = transform.localScale;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            Vector3 playerScale = transform.localScale;
            Vector3 inverseScale = new Vector3(1/(playerScale.x/startScale.x), 1/(playerScale.y/startScale.y), 1/(playerScale.z/startScale.z));
            weapon.localScale = inverseScale;
            weapon.localPosition = new Vector3(weapon.localPosition.x, crouchYScale - startYScale*0.9f, weapon.localPosition.z);

            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Push the player model down so they are still on the ground. 
        }
        
        // Stop crouching
        if(Input.GetKeyUp(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            Vector3 playerScale = transform.localScale;
            Vector3 inverseScale = new Vector3(1/playerScale.x, 1/playerScale.y, 1/playerScale.z);
            weapon.localScale = inverseScale;
            weapon.localPosition = new Vector3(weapon.localPosition.x, weapon.localPosition.y + startYScale - crouchYScale, weapon.localPosition.z);
        }
    }

    private void MovePlayer(){
        
        moveDirection = orientation.forward * verticleInput + orientation.right * horizontalInput; // Move the direction you are looking
        
        // While on the ground 
        if(grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); // Actually move the player in that direction

            // Play footstep sound
            footstepTimer -= Time.deltaTime;
            if(rb.velocity.magnitude > 0.15f) {
                if(footstepTimer <= 0) {
                    audioSource.pitch = Random.Range(0.9f, 1f);
                    audioSource.PlayOneShot(walkingClips[Random.Range(0,walkingClips.Length-1)]);
                    if(state == MovementState.sprinting){
                        footstepTimer = stepSpeed * ((walkSpeed + PlayerPrefs.GetInt("speedBoost")) / sprintSpeed); // Adjust by a factor of walk speed over sprint speed
                    } else if(state == MovementState.crouching){
                        footstepTimer = stepSpeed * ((walkSpeed + PlayerPrefs.GetInt("speedBoost")) / crouchSpeed); // Adjust by a factor of walk speed over crouch speed
                    } else {
                        footstepTimer = stepSpeed;
                    }
                }
            }
        }
        // in the air
        else {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); // Actually move the player in that direction
            rb.AddForce(Physics.gravity*rb.mass*(gravityMultiplier-1f));
        }
    }

    // Limit the max speed
    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Determine if the speed is too fast and limit it
        if(flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        // Reset the y velocity for consistent jump
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        audioSource.PlayOneShot(jumpingClips[Random.Range(0,jumpingClips.Length-1)]);

    }

    private void ResetJump() {
        readyToJump = true;
    }

    private void StateHandler() {
        // Crouching
        if(grounded && Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        // Sprinting
        else if(grounded && Input.GetKey(sprintKey)) {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        // Walking
        else if (grounded) {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // In the air
        else {
            state = MovementState.air;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Disable jumping upon entering lava
        if (other.gameObject.CompareTag("Lava")) {
            Debug.Log("PLAYER: Entered Lava");            
            jumpEnabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Re-enable jump after leaving lava
        if (other.gameObject.CompareTag("Lava")) {
            Debug.Log("PLAYER: Exited Lava");
            jumpEnabled = true;
        }
    }
}
