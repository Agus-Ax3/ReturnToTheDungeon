using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;

    [Header("References")]
    [SerializeField] private PlayerBlock playerBlock;
    [SerializeField] private PlayerDodge playerDodge;
    [SerializeField] private Animator animator;
    public bool IsDead { get; private set; }
    public bool IsHit { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        if (playerDodge != null && playerDodge.IsInvulnerable)
        {
            Debug.Log("Daño evitado por esquiva");
            return;
        }

        CurrentHealth = Mathf.Max(
            CurrentHealth - damage, 0f
        );

        Debug.Log("Vida del jugador: " + CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
        else
        {
            PlayHitReaction();
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
        IsHit = false;

        animator.ResetTrigger("Hit");
        animator.SetBool("IsHit", false);
        animator.SetBool("IsDead", true);

        Debug.Log("Jugador derrotado");
    }

    public void TakeDamage(float damage, Vector3 attackerPosition)
    {
        if (IsDead || damage <= 0f)
            return;

        // Comprobar invulnerabilidad antes del bloqueo
        if (playerDodge != null && playerDodge.IsInvulnerable)
        {
            Debug.Log("Daño evitado por esquiva");
            return;
        }

        if (playerBlock != null &&
            playerBlock.TryBlock(attackerPosition))
        {
            return;
        }

        TakeDamage(damage);
    }
    private void PlayHitReaction()
    {
        IsHit = true;

        animator.SetBool("IsHit", true);
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Hit");
    }

    public void EndHitReaction()
    {
        IsHit = false;

        animator.SetBool("IsHit", false);
    }
}