using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    private bool collected = false;
    private Room room;

    private void Awake()
    {
        room = GetComponentInParent<Room>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        PlayerUpgrades playerUpgrades =
            other.GetComponentInParent<PlayerUpgrades>();

        if (playerUpgrades == null)
            return;

        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null || playerHealth.IsDead)
            return;

        if (!playerUpgrades.ApplyRandomUpgrade())
            return;

        collected = true;

        if (room != null)
        {
            room.Complete();
        }

        Destroy(gameObject);
    }
}