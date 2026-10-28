using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private float dropForce = 4f;
    private int currentHealth;
    private CombatRoom combatRoom;
    private Renderer rend;

    private void Awake()
    {
        currentHealth = maxHealth;
        rend = GetComponent<Renderer>();
    }

    public void SetCombatRoom(CombatRoom room)
    {
        combatRoom = room;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Se pone más rojo cuanto menos vida le queda
        float t = (float)currentHealth / maxHealth;
        rend.material.color = Color.Lerp(Color.red, Color.white, t);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (dropPrefab != null)
        {
            Vector3 dropPosition = transform.position + Vector3.up;
            GameObject drop = Instantiate(dropPrefab, dropPosition, Quaternion.identity);

            Rigidbody rb = drop.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
        }

        if (combatRoom != null)
        {
            combatRoom.EnemyDefeated();
        }
        Destroy(gameObject);
    }
}
