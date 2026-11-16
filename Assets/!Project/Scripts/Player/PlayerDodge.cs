
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[DefaultExecutionOrder(-10)]
public class PlayerDodge : MonoBehaviour
{
    [Header("Roll Settings")]
    [SerializeField] private float rollSpeed = 6f;
    [SerializeField] private float rollDuration = 0.8f;

    [Header("Directional Dodge Settings")]
    [SerializeField] private float dodgeSpeed = 4f;
    [SerializeField] private float dodgeDuration = 0.45f;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LockOnSystem lockOnSystem;
    [SerializeField] private PlayerCombat playerCombat;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference dodgeAction;

    [Header("Stamina")]
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private float rollStaminaCost = 25f;
    [SerializeField] private float dodgeStaminaCost = 20f;

    private CharacterController controller;

    private Vector3 dodgeDirection;
    private float dodgeTimer;
    private float currentDodgeSpeed;

    public bool IsDodging { get; private set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        dodgeAction?.action.Enable();
    }

    private void OnDisable()
    {
        dodgeAction?.action.Disable();
    }

    private void Update()
    {
        if (IsDodging)
        {
            UpdateDodge();
            return;
        }

        if (dodgeAction != null &&
            dodgeAction.action.WasPressedThisFrame())
        {
            TryDodge();
        }
    }


    private void TryDodge()
    {
        if (playerCombat != null && playerCombat.IsAttacking)
            return;
        if (!controller.isGrounded)
            return;

        if (animator.IsInTransition(0))
            return;

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Locomotion") &&
            !state.IsName("LockOnLocomotion"))
            return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        bool isLockedOn = lockOnSystem != null &&
                          lockOnSystem.IsLockedOn;

        bool hasMovement = input.sqrMagnitude > 0.01f;

        bool useRoll = !isLockedOn && hasMovement;

        // Consumo de stamina
        float staminaCost = useRoll
            ? rollStaminaCost
            : dodgeStaminaCost;

        if (playerStamina == null ||
            !playerStamina.ConsumeStamina(staminaCost))
        {
            return;
        }

        if (useRoll)
        {
            StartRoll(input);
        }
        else
        {
            StartDirectionalDodge(input);
        }

        IsDodging = true;

        animator.SetBool("IsDodging", true);
        animator.SetBool("UseRoll", useRoll);
        animator.SetTrigger("Dodge");
    }


    private void StartRoll(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        dodgeDirection =
            (forward * input.y + right * input.x).normalized;

        transform.rotation =
            Quaternion.LookRotation(dodgeDirection);

        currentDodgeSpeed = rollSpeed;
        dodgeTimer = rollDuration;
    }

    private void StartDirectionalDodge(Vector2 input)
    {
        Vector2 direction;

        // Sin movimiento: esquiva hacia atras
        if (input.sqrMagnitude < 0.01f)
        {
            direction = Vector2.down;
        }
        else if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            direction = new Vector2(Mathf.Sign(input.x), 0f);
        }
        else
        {
            direction = new Vector2(0f, Mathf.Sign(input.y));
        }

        dodgeDirection =
            (transform.right * direction.x +
             transform.forward * direction.y).normalized;

        animator.SetFloat("DodgeX", direction.x);
        animator.SetFloat("DodgeY", direction.y);

        currentDodgeSpeed = dodgeSpeed;
        dodgeTimer = dodgeDuration;
    }

    private void UpdateDodge()
    {
        float delta = Mathf.Min(Time.deltaTime, dodgeTimer);

        Vector3 velocity =
            dodgeDirection * currentDodgeSpeed;

        velocity.y = -5f;

        controller.Move(velocity * delta);

        dodgeTimer -= delta;

        if (dodgeTimer <= 0f)
        {
            EndDodge();
        }
    }

    private void EndDodge()
    {
        IsDodging = false;

        animator.SetBool("IsDodging", false);
    }
}
