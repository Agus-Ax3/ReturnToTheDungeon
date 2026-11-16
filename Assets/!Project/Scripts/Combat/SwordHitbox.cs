using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 25f;

    private BoxCollider hitbox;

    private HashSet<EnemyHealth> enemiesHit =
        new HashSet<EnemyHealth>();

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
        EnemyHealth enemy =
            other.GetComponentInParent<EnemyHealth>();

        if (enemy == null)
            return;

        if (enemiesHit.Add(enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}