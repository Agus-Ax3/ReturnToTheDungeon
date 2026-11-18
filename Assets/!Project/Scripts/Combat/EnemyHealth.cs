using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    [Header("References")]
    [SerializeField] private EnemyAI enemyAI;

    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;

        if (enemyAI == null)
            enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        CurrentHealth = Mathf.Max(
            CurrentHealth - damage, 0f
        );

        Debug.Log("Vida del enemigo: " + CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
        else
        {
            if (enemyAI != null)
                enemyAI.ReceiveHit();
        }
    }

    private void Die()
    {
        IsDead = true;

        if (enemyAI != null)
            enemyAI.Die();

        Debug.Log("Enemigo derrotado");
    }
}