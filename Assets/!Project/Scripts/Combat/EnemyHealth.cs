using UnityEngine;

public class EnemyHealth : Health
{
    [Header("References")]
    [SerializeField] private EnemyAI enemyAI;

    protected override void Awake()
    {
        base.Awake();

        if (enemyAI == null)
            enemyAI = GetComponent<EnemyAI>();
    }

    protected override void OnHit()
    {
        if (enemyAI != null)
            enemyAI.ReceiveHit();

        Debug.Log("Vida del enemigo: " + CurrentHealth);
    }

    protected override void Die()
    {
        if (enemyAI != null)
            enemyAI.Die();

        Debug.Log("Enemigo derrotado");
    }
}