using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyWeaponHitbox weaponHitbox;

    public void EnableHitbox()
    {
        if (enemyAI == null ||
            enemyAI.IsDead ||
            enemyAI.IsHit ||
            !enemyAI.IsAttacking)
            return;

        if (weaponHitbox != null)
            weaponHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        if (weaponHitbox != null)
            weaponHitbox.DisableHitbox();
    }

    public void EndAttack()
    {
        DisableHitbox();

        if (enemyAI != null)
            enemyAI.EndAttack();
    }

    public void EndHit()
    {
        if (enemyAI != null)
            enemyAI.EndHit();
    }
}