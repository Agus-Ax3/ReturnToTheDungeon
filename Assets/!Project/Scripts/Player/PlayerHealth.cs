using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;

    [Header("References")]
    [SerializeField] private PlayerBlock playerBlock;
    public bool IsDead { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        CurrentHealth = Mathf.Max(
            CurrentHealth - damage, 0f
        );

        Debug.Log("Vida del jugador: " + CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead || amount <= 0f)
            return;

        CurrentHealth = Mathf.Min(
            CurrentHealth + amount, maxHealth
        );
    }

    private void Die()
    {
        IsDead = true;

        Debug.Log("Jugador derrotado");
    }

    public void TakeDamage(float damage, Vector3 attackerPosition)
    {
        if (IsDead || damage <= 0f)
            return;

        if (playerBlock != null &&
            playerBlock.TryBlock(attackerPosition))
        {
            return;
        }

        TakeDamage(damage);
    }
}