using UnityEngine;

public class CompleteZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Room room = GetComponentInParent<Room>();
        room.Complete();
    }
}
