using UnityEngine;

public class PlayerHealth : Health
{
    [Header("References")]
    [SerializeField] private PlayerBlock playerBlock;
    [SerializeField] private PlayerDodge playerDodge;
    [SerializeField] private Animator animator;

    public bool IsHit { get; private set; }

    public override void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        if (playerDodge != null &&
            playerDodge.IsInvulnerable)
        {
            Debug.Log("Daño evitado por esquiva");
            return;
        }

        base.TakeDamage(damage);

        Debug.Log("Vida del jugador: " + CurrentHealth);
    }

    public void TakeDamage(
        float damage, Vector3 attackerPosition)
    {
        if (IsDead || damage <= 0f)
            return;

        if (playerDodge != null &&
            playerDodge.IsInvulnerable)
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

    protected override void Die()
    {
        IsHit = false;

        animator.ResetTrigger("Hit");
        animator.SetBool("IsHit", false);
        animator.SetBool("IsDead", true);

        Debug.Log("Jugador derrotado");
    }

    protected override void OnHit()
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