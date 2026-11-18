using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;

    public Transform TargetPoint => transform;

    public bool IsTargetable =>
        isActiveAndEnabled &&
        (enemyHealth == null || !enemyHealth.IsDead);

    private void Awake()
    {
        if (enemyHealth == null)
            enemyHealth = GetComponentInParent<EnemyHealth>();
    }
}