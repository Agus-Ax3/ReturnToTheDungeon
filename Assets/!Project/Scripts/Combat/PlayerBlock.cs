using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerDodge playerDodge;

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
    }

    private void SetBlocking(bool value)
    {
        IsBlocking = value;

        if (animator != null)
            animator.SetBool("IsBlocking", value);
    }

    public void PlayBlockImpact()
    {
        if (IsBlocking)
            animator.SetTrigger("BlockHit");
    }
}