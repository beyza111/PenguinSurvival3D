using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public bool hasKey = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
    }

    public Camera playerCamera; // First-person camera
    public Camera thirdPersonCamera; // Third-person camera
    public Animator animator;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    bool isFirstPerson = true; // Flag to track current camera mode

    // New variables for snowball throw mechanic
    public GameObject snowballPrefab;
    public float throwForce = 15f;

    // Aiming variables
    private Vector3 aimDirection;
    public float aimSensitivity = 1f;
    public LineRenderer trajectoryLine;
    public int trajectoryResolution = 30;
    public float trajectoryTimeStep = 0.1f;

    private bool isAiming = false; // Track if the player is aiming

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        LockCursor();

        // Initially disable third person camera
        thirdPersonCamera.gameObject.SetActive(false);

        // Initialize aiming direction to be forward
        aimDirection = playerCamera.transform.forward;

        // Disable trajectory line initially
        trajectoryLine.enabled = false;
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Update animator's IsMoving parameter based on player movement
        bool isMoving = (moveDirection.x != 0 || moveDirection.z != 0);
        animator.SetBool("IsMoving", isMoving);

        // Check for 'C' key press to switch cameras
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson; // Toggle camera mode

            // Enable/disable cameras based on camera mode
            playerCamera.gameObject.SetActive(isFirstPerson);
            thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
        }

        // Handle aiming and throwing
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartAiming();
        }

        if (Input.GetKey(KeyCode.J))
        {
            UpdateAimDirection();
            UpdateTrajectoryLine();
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            ThrowSnowball();
        }
    }

    void StartAiming()
    {
        isAiming = true;
        trajectoryLine.enabled = true;
    }

    void StopAiming()
    {
        isAiming = false;
        trajectoryLine.enabled = false;
    }

    void UpdateAimDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            aimDirection += playerCamera.transform.up * aimSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            aimDirection -= playerCamera.transform.up * aimSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            aimDirection -= playerCamera.transform.right * aimSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            aimDirection += playerCamera.transform.right * aimSensitivity * Time.deltaTime;
        }

        aimDirection.Normalize();
    }

    void UpdateTrajectoryLine()
    {
        Vector3[] points = new Vector3[trajectoryResolution];
        Vector3 currentPosition = playerCamera.transform.position;
        Vector3 currentVelocity = aimDirection * throwForce;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            points[i] = currentPosition;
            currentPosition += currentVelocity * trajectoryTimeStep;
            currentVelocity += Physics.gravity * trajectoryTimeStep;
        }

        trajectoryLine.positionCount = points.Length;
        trajectoryLine.SetPositions(points);
    }

    void ThrowSnowball()
    {
        // Instantiate the snowball at the player's position + forward direction
        GameObject snowball = Instantiate(snowballPrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);

        // Get the Rigidbody component and add force to it
        Rigidbody rb = snowball.GetComponent<Rigidbody>();

        // Use the aim direction for throwing
        rb.AddForce(aimDirection * throwForce, ForceMode.VelocityChange);

        // Stop aiming after throwing
        StopAiming();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canMove = false;

        // Ensure the character stays grounded
        moveDirection = Vector3.zero;
    }
}
