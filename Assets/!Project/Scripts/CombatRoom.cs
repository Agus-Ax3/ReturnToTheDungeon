using UnityEngine;

public class CombatRoom : MonoBehaviour
{
    private int enemiesAlive = 0;
    private Room room;

    private void Start()
    {
        room = GetComponent<Room>();

        Enemy[] enemies = GetComponentsInChildren<Enemy>();
        enemiesAlive = enemies.Length;

        foreach (Enemy enemy in enemies)
        {
            enemy.SetCombatRoom(this);
        }

        Debug.Log(name + ": " + enemiesAlive + " enemigos");

        if (enemiesAlive == 0)
        {
            room.Complete();
        }
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            room.Complete();
        }
    }
}
