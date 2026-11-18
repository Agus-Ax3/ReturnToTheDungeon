using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyWeaponHitbox : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 20f;

    [Header("References")]
    [SerializeField] private Transform attacker;

    private BoxCollider hitbox;

    private HashSet<PlayerHealth> hitPlayers =
        new HashSet<PlayerHealth>();

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider>();
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hitPlayers.Clear();
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitbox.enabled || attacker == null)
            return;

        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
            return;

        if (hitPlayers.Contains(playerHealth))
            return;

        hitPlayers.Add(playerHealth);

        playerHealth.TakeDamage(
            damage,
            attacker.position
        );
    }
}