using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private AudioClip[] walkingClips;
    [SerializeField] private float stepSpeed;
    private float footstepTimer;


    
    public Transform orientation;
    float horizontalInput;
    float verticleInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        footstepTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground); 

        MyInput();
        SpeedControl();

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
        if(Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // Wait for the jump to cooldown before allowing a second jump
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
                    audioSource.PlayOneShot(walkingClips[Random.Range(0,walkingClips.Length-1)]);
                    footstepTimer = stepSpeed;
                }
            }
        }
        // in the air
        else {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); // Actually move the player in that direction
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
    }

    private void ResetJump() {
        readyToJump = true;
    }
}
