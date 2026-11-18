
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private PlayerDodge playerDodge;
    

    [Header("Input Actions")]
    [SerializeField] private InputActionReference attackAction;

    [Header("Stamina Costs")]
    [SerializeField] private float attack1Cost = 15f;
    [SerializeField] private float attack2Cost = 15f;
    [SerializeField] private float attack3Cost = 25f;

    [Header("Combo Settings")]
    [SerializeField] private float comboWindowStart = 0.25f;
    [SerializeField] private float comboWindowEnd = 0.75f;

    private CharacterController controller;

    private int currentAttack;
    private bool comboQueued;
    private bool attackStarted;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        attackAction?.action.Enable();
    }

    private void OnDisable()
    {
        attackAction?.action.Disable();
    }

    private void Update()
    {
        UpdateAttackState();

        if (attackAction != null &&
            attackAction.action.WasPressedThisFrame())
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (!controller.isGrounded)
            return;

        if (playerDodge != null && playerDodge.IsDodging)
            return;

        if (!IsAttacking)
        {
            StartAttack();
        }
        else
        {
            TryCombo();
        }
    }


    private void StartAttack()
    {
        if (animator.GetBool("IsBlocking"))
            return;

        if (animator.IsInTransition(0))
            return;

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        bool canAttack =
            state.IsName("Locomotion") ||
            state.IsName("LockOnLocomotion") ||
            state.IsName("Block2");

        if (!canAttack)
            return;

        if (!playerStamina.ConsumeStamina(attack1Cost))
            return;

        currentAttack = 1;
        comboQueued = false;
        attackStarted = false;

        IsAttacking = true;

        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("Attack");
    }


    private void TryCombo()
    {
        if (currentAttack >= 3 || comboQueued)
            return;

        if (animator.IsInTransition(0))
            return;

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Attack" + currentAttack))
            return;

        float progress = state.normalizedTime;

        if (progress < comboWindowStart ||
            progress >= comboWindowEnd)
            return;

        float staminaCost = currentAttack == 1
            ? attack2Cost
            : attack3Cost;

        if (!playerStamina.ConsumeStamina(staminaCost))
            return;

        comboQueued = true;

        animator.SetTrigger("ComboAttack");
    }

    private void UpdateAttackState()
    {
        if (!IsAttacking)
            return;

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        int detectedAttack = 0;

        if (state.IsName("Attack1"))
            detectedAttack = 1;
        else if (state.IsName("Attack2"))
            detectedAttack = 2;
        else if (state.IsName("Attack3"))
            detectedAttack = 3;

        if (detectedAttack > 0)
        {
            attackStarted = true;

            if (detectedAttack != currentAttack)
            {
                currentAttack = detectedAttack;
                comboQueued = false;
            }
        }
        else if (attackStarted &&
                 !animator.IsInTransition(0))
        {
            EndAttack();
        }
    }

    private void EndAttack()
    {
        IsAttacking = false;

        currentAttack = 0;
        comboQueued = false;
        attackStarted = false;

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("ComboAttack");
        animator.SetBool("IsAttacking", false);
    }
}
