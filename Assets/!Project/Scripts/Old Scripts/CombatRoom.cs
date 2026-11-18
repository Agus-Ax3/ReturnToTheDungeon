using UnityEngine;

public class CombatRoom : MonoBehaviour
{
    private Room room;
    private EnemyHealth[] enemies;

    private int enemiesAlive;
    private bool completed = false;

    private void Start()
    {
        room = GetComponent<Room>();

        enemies = GetComponentsInChildren<EnemyHealth>();

        enemiesAlive = 0;

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy != null && !enemy.IsDead)
            {
                enemiesAlive++;
                enemy.OnDeath += OnEnemyDeath;
            }
        }

        Debug.Log(name + ": " + enemiesAlive + " enemigos");

        if (enemiesAlive == 0)
        {
            CompleteRoom();
        }
    }

    private void OnEnemyDeath()
    {
        if (completed)
            return;

        enemiesAlive--;

        Debug.Log(name + ": " + enemiesAlive
            + " enemigos restantes");

        if (enemiesAlive == 0)
        {
            CompleteRoom();
        }
    }

    private void CompleteRoom()
    {
        if (completed)
            return;

        completed = true;

        if (room != null)
        {
            room.Complete();
        }
    }

    private void OnDestroy()
    {
        if (enemies == null)
            return;

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnDeath -= OnEnemyDeath;
            }
        }
    }

    public void EnemyDefeated()
    {
    }
}