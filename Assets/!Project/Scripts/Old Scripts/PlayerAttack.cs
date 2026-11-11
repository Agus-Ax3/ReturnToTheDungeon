using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float range = 1.5f;
    [SerializeField] private float cooldown = 0.5f;

    private float nextAttackTime = 0f;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + cooldown;
        }
    }

    private void Attack()
    {
        Vector3 center = transform.position + transform.forward * range;
        Collider[] hits = Physics.OverlapSphere(center, range);

        foreach (Collider hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * range, range);
    }
}
