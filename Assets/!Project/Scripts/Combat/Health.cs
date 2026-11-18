using System;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;
    public bool IsDead { get; private set; }

    public event Action OnDeath;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f)
            return;

        CurrentHealth = Mathf.Max(
            CurrentHealth - damage, 0f
        );

        if (CurrentHealth <= 0f)
        {
            IsDead = true;
            Die();

            OnDeath?.Invoke();
        }
        else
        {
            OnHit();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead || amount <= 0f)
            return;

        CurrentHealth = Mathf.Min(
            CurrentHealth + amount, MaxHealth
        );
    }

    protected abstract void Die();

    protected abstract void OnHit();
}