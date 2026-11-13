using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.8f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float fallMultiplier = 2.5f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LockOnSystem lockOnSystem;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationDampTime = 0.1f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference jumpAction;

    private CharacterController controller;

    private float verticalVelocity;
    private float coyoteTimer;
    private float jumpBufferTimer;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void OnEnable()
    {
        moveAction?.action.Enable();
        sprintAction?.action.Enable();
        jumpAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        sprintAction?.action.Disable();
        jumpAction?.action.Disable();
    }

    private void Update()
    {
        HandleJump();
        Move();
    }

    private void HandleJump()
    {
        // Recordar la pulsacion de salto
        if (jumpAction != null &&
            jumpAction.action.WasPressedThisFrame())
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        // Permitir saltar poco despues de dejar una plataforma
        if (controller.isGrounded)
        {
            coyoteTimer = coyoteTime;

            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // Ejecutar el salto
        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );

            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }
    }

    private void Move()
    {
        if (cameraTransform == null || moveAction == null)
            return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Movimiento relativo a la camara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction =
            forward * input.y + right * input.x;

        direction = Vector3.ClampMagnitude(direction, 1f);

        bool isRunning = sprintAction != null &&
                         sprintAction.action.IsPressed();

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Rotacion del personaje
        if (lockOnSystem != null &&
            lockOnSystem.IsLockedOn &&
            lockOnSystem.CurrentTarget != null)
        {
            Vector3 targetDirection =
                lockOnSystem.CurrentTarget.position -
                transform.position;

            targetDirection.y = 0f;

            if (targetDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(targetDirection);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
        else if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Gravedad
        if (verticalVelocity < 0f)
        {
            // Caida mas rapida
            verticalVelocity +=
                gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            // Ascenso normal
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 velocity = direction * currentSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        // Actualizar las animaciones
        UpdateAnimations(direction.magnitude, isRunning);
    }

    private void UpdateAnimations(
        float movementAmount, bool isRunning)
    {
        if (animator == null)
            return;

        float targetSpeed = 0f;

        if (movementAmount > 0.01f)
        {
            targetSpeed = isRunning ? 1f : 0.5f;
            targetSpeed *= movementAmount;
        }

        animator.SetFloat(
            SpeedHash,
            targetSpeed,
            animationDampTime,
            Time.deltaTime
        );
    }
}