using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyWeaponHitbox weaponHitbox;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private float attackTimer;

    public bool IsAttacking { get; private set; }
    public bool IsHit { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (IsDead)
            return;

        if (player == null || animator == null ||
            !agent.isOnNavMesh)
            return;

        attackTimer -= Time.deltaTime;

        if (IsAttacking || IsHit)
        {
            StopMovement();
            UpdateAnimations();
            return;
        }

        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance <= attackRange)
        {
            StopMovement();
            FacePlayer();

            if (attackTimer <= 0f)
            {
                TryAttack();
            }
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            StopMovement();
        }

        UpdateAnimations();
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void StopMovement()
    {
        if (agent.enabled && agent.isOnNavMesh)
            agent.isStopped = true;
    }

    private void TryAttack()
    {
        if (IsDead || IsHit || IsAttacking)
            return;

        if (animator.IsInTransition(0))
            return;

        AnimatorStateInfo state =
            animator.GetCurrentAnimatorStateInfo(0);

        if (!state.IsName("Locomotion"))
            return;

        IsAttacking = true;

        StopMovement();

        animator.SetTrigger("Attack");
    }

    public void EndAttack()
    {
        IsAttacking = false;
        attackTimer = attackCooldown;
    }

    public void ReceiveHit()
    {
        if (IsDead)
            return;

        IsAttacking = false;
        IsHit = true;

        if (weaponHitbox != null)
            weaponHitbox.DisableHitbox();

        StopMovement();

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Hit");
        animator.SetFloat("Speed", 0f);
    }

    public void EndHit()
    {
        if (IsDead)
            return;

        IsHit = false;
        attackTimer = attackCooldown;
    }

    public void Die()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsAttacking = false;
        IsHit = false;

        if (weaponHitbox != null)
            weaponHitbox.DisableHitbox();

        StopMovement();

        if (agent.enabled && agent.isOnNavMesh)
            agent.ResetPath();

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");

        animator.SetFloat("Speed", 0f);
        animator.SetBool("IsDead", true);

        agent.enabled = false;
    }

    private void FacePlayer()
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            8f * Time.deltaTime
        );
    }

    private void UpdateAnimations()
    {
        float speed = agent.isStopped
            ? 0f
            : agent.velocity.magnitude / agent.speed;

        animator.SetFloat(
            "Speed",
            speed,
            0.1f,
            Time.deltaTime
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position, detectionRange
        );

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position, attackRange
        );
    }
}