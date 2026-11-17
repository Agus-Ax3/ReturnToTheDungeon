using UnityEngine;

public class HitReactionBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        PlayerHealth playerHealth =
            animator.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null &&
            !playerHealth.IsDead)
        {
            playerHealth.EndHitReaction();
        }
    }
}