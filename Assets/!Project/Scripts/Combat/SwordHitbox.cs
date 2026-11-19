using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    private float damageMultiplier = 1f;

    private BoxCollider hitbox;

    private HashSet<IDamageable> enemiesHit =
        new HashSet<IDamageable>();

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    public void EnableHitbox(float attackDamage)
    {
        damage = attackDamage;
        enemiesHit.Clear();
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target =
            other.GetComponentInParent<IDamageable>();

        if (!(target is EnemyHealth))
            return;

        if (enemiesHit.Add(target))
        {
            target.TakeDamage(damage * damageMultiplier);
        }
    }
    public void IncreaseDamage(float percentage)
    {
        if (percentage <= 0f)
            return;

        damageMultiplier += percentage / 100f;
    }
}