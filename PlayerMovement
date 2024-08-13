using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variables
    public CharacterController controller;
    public Camera playerCamera;
    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float gravity = -9.81f;
    public float sprintDuration = 10f;
    public float normalFOV = 60f;
    public float sprintFOV = 90f;
    public float fovTransitionSpeed = 5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float stepHeight = 0.3f; 
    public LayerMask stairsMask;

    private Vector3 velocity;
    private bool isGrounded;
    private float sprintTimeRemaining;
    private bool isSprinting;

    // before game starts
    void Start()
    {
        sprintTimeRemaining = sprintDuration;
        isSprinting = false;
        playerCamera.fieldOfView = normalFOV; // Set the initial FOV
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 move = playerCamera.transform.right * x + playerCamera.transform.forward * z;
        move.y = 0; // Ensure the movement direction is on the same plane

        bool isMoving = move.magnitude > 0.1f;

        // Sprint logic
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimeRemaining > 0 && isMoving)
        {
            isSprinting = true;
            sprintTimeRemaining -= Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            if (sprintTimeRemaining < sprintDuration)
            {
                sprintTimeRemaining += Time.deltaTime;
            }
        }

        // Ensure FOV returns to normal if sprint time is depleted
        if (sprintTimeRemaining <= 0)
        {
            isSprinting = false;
        }

        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Check for stairs and handle climbing
        HandleStairs(ref move);

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Adjust FOV based on sprinting
        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, fovTransitionSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStairs(ref Vector3 move)
    {
        RaycastHit hit;
        Vector3 origin = transform.position + new Vector3(0, 0.5f, 0); // Adjust the ray origin height if necessary

        // Cast a ray forward to detect stairs
        if (Physics.Raycast(origin, transform.forward, out hit, 1f, stairsMask))
        {
            if (hit.collider != null)
            {
                move.y = stepHeight; // Adjust the player's upward movement to simulate stair climbing
            }
        }
    }
}
