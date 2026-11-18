using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [SerializeField] private List<Room> rooms;
    [SerializeField] private Room bossRoom;

    private int currentIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        ShuffleRooms();
    }

    private void ShuffleRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int randomIndex = Random.Range(i, rooms.Count);

            Room temp = rooms[i];
            rooms[i] = rooms[randomIndex];
            rooms[randomIndex] = temp;
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log("Sala " + (i + 1) + ": " + rooms[i].name);
        }
    }

    public Transform GetNextDestination()
    {
        currentIndex++;

        if (currentIndex < rooms.Count)
        {
            return rooms[currentIndex].GetSpawnPoint();
        }

        return bossRoom.GetSpawnPoint();
    }
}