using UnityEngine;
using System.Collections.Generic;
public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Room> rooms;   // todas menos lobby y boss
    [SerializeField] private Room bossRoom;

    private int currentIndex = -1; // -1 = todavía estamos en el lobby

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

        // Para ver el orden en la consola
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

        return bossRoom.GetSpawnPoint(); // ya no quedan salas: vamos al jefe
    }
}
