using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Jump and Gravity")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    [Header("Ground Detect")]   //mejor para en rampas y bordes 
    [SerializeField] private Transform groundCheck; //donde esta la esfera 
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundMask;   // capas que cuentan como suelo (excluye Player para que no se detecte a sí mismo)

    [Header("References")]
    [SerializeField] private Transform cameraTransforms;

    private CharacterController controller;
    private InputSystem_Actions controls;
    private float verticalVelocity;
    private bool isGrounded;


    void Awake()
    {
        controller= GetComponent<CharacterController>();
        controls = new InputSystem_Actions();

        if (cameraTransforms == null && Camera.main != null)
        { 
            cameraTransforms = Camera.main.transform;
        }


    }

    private void OnEnable()   // onEnable y OnDisable se usa para  activacion y desactivacion de las teclas al pausar o morir el jugador.
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    void Update()
    {
        CheckGround();
        Vector2 input= controls.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection= GetCameraRelativeDirection(input);
        RotateTowards(moveDirection);
        float currentSpeed = controls.Player.Sprint.IsPressed() ? sprintSpeed : speed;
        
        HandleJumpAndGravity();
        Vector3 velocity = moveDirection * currentSpeed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundCheckRadius,groundMask,QueryTriggerInteraction.Ignore);
    }
    private  Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        Vector3 camForward = cameraTransforms.forward;
        Vector3 camRight = cameraTransforms.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction= camForward * input.y + camRight * input.x;
        return Vector3.ClampMagnitude(direction, 1f);

    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)return;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void HandleJumpAndGravity()
    {
        if(isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        if(isGrounded && controls.Player.Jump.WasPressedThisFrame())
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color=isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}