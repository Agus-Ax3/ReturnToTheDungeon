
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

        enemiesAlive = enemies.Length;

        Debug.Log(name + ": " + enemiesAlive + " enemigos");

        if (enemiesAlive == 0)
        {
            CompleteRoom();
        }
    }

    private void Update()
    {
        if (completed) return;

        int alive = 0;

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy != null && !enemy.IsDead)
            {
                alive++;
            }
        }

        if (alive != enemiesAlive)
        {
            enemiesAlive = alive;

            Debug.Log(name + ": " + enemiesAlive
                + " enemigos restantes");
        }

        if (enemiesAlive == 0)
        {
            CompleteRoom();
        }
    }

    private void CompleteRoom()
    {
        if (completed) return;

        completed = true;

        if (room != null)
        {
            room.Complete();
        }
    }

    
    public void EnemyDefeated()
    {
    }
}
