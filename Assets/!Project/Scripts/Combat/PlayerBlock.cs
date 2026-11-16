using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerDodge playerDodge;

    [Header("Block Settings")]
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private float blockStaminaCost = 20f;
    [SerializeField] private float blockAngle = 120f;
    [SerializeField] private float guardBreakDuration = 1f;

    private float guardBreakTimer;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference blockAction;

    public bool IsBlocking { get; private set; }

    private void OnEnable()
    {
        blockAction?.action.Enable();
    }

    private void OnDisable()
    {
        blockAction?.action.Disable();
        SetBlocking(false);
    }

    private void Update()
    {

        

        if (blockAction == null || animator == null)
            return;

        bool wantsToBlock = blockAction.action.IsPressed();

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        bool canBlock =
     state.IsName("Locomotion") ||
     state.IsName("LockOnLocomotion") ||
     state.IsName("Block1") ||
     state.IsName("BlockIdle") ||
     state.IsName("BlockImpact") ||
     state.IsName("Block2");

        if (playerDodge != null && playerDodge.IsDodging)
            canBlock = false;

        if (animator.GetBool("IsAttacking"))
            canBlock = false;

        SetBlocking(wantsToBlock && canBlock);
        if (guardBreakTimer > 0f)
        {
            guardBreakTimer -= Time.deltaTime;
            canBlock = false;
        }
    }

    private void SetBlocking(bool value)
    {
        IsBlocking = value;

        if (animator != null)
            animator.SetBool("IsBlocking", value);
    }

    public void PlayBlockImpact()
    {
        if (!IsBlocking)
            return;

        animator.ResetTrigger("BlockHit");
        animator.Play("BlockImpact", 0, 0f);
    }
    public bool TryBlock(Vector3 attackerPosition)
    {
        if (!IsBlocking || playerStamina == null)
            return false;

        Vector3 direction = attackerPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return false;

        float angle = Vector3.Angle(
            transform.forward,
            direction
        );

        if (angle > blockAngle / 2f)
            return false;

        if (!playerStamina.HasStamina(blockStaminaCost))
        {
            playerStamina.ConsumeStamina(
                playerStamina.CurrentStamina
            );

            guardBreakTimer = guardBreakDuration;
            SetBlocking(false);

            Debug.Log("Guardia rota");

            return false;
        }

        playerStamina.ConsumeStamina(blockStaminaCost);

        PlayBlockImpact();

        Debug.Log("Ataque bloqueado");

        return true;
    }
}