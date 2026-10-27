using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}
