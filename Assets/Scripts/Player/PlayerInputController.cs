using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Components")]
    private Animator anim;
    private Rigidbody rb;

    private Vector2 moveInput;
    private bool isRunning; // somente registra se shift tá pressionado

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

   private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimations();
    }

    // === INPUT ===
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }

    // === MOVEMENT ===
    private void HandleMovement()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 velocity = moveDir * speed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    // === ROTATION ===
    private void HandleRotation()
    {
        if (moveInput.sqrMagnitude <= 0.01f) return;

        Vector3 targetDir = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion targetRot = Quaternion.LookRotation(targetDir);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    // === ANIMS ===
    private void HandleAnimations()
    {
        float mag = moveInput.magnitude;

        bool isMoving = mag > 0.1f;

        if(!isMoving) isRunning = false;

        anim.SetBool("isWalking", isMoving && !isRunning);
        anim.SetBool("isRunning", isMoving && isRunning);
        anim.SetBool("isIdle", !isMoving);
    }
}