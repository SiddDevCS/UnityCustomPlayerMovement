using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
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

    void Start()
    {
        sprintTimeRemaining = sprintDuration;
        isSprinting = false;
        playerCamera.fieldOfView = normalFOV; // Set initial FOV
    }

    void Update()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f; // Prevent unnatural snapping to the ground
        }

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (playerCamera.transform.right * x + playerCamera.transform.forward * z).normalized;
        move.y = 0; // Keep movement on the horizontal plane

        bool isMoving = move.magnitude > 0.1f;

        // Sprint Logic
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimeRemaining > 0 && isMoving)
        {
            isSprinting = true;
            sprintTimeRemaining -= Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            sprintTimeRemaining = Mathf.Min(sprintDuration, sprintTimeRemaining + Time.deltaTime);
        }

        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Stair Handling
        HandleStairs(ref move);

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Adjust FOV smoothly
        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, fovTransitionSpeed * Time.deltaTime);

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStairs(ref Vector3 move)
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, transform.forward, out hit, 1f, stairsMask))
        {
            if (hit.collider != null)
            {
                move.y = stepHeight;
            }
        }
    }
}
