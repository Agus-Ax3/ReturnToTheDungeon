using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

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
        if (combatRoom != null)
        {
            combatRoom.EnemyDefeated();
        }
        Destroy(gameObject);
    }
}
