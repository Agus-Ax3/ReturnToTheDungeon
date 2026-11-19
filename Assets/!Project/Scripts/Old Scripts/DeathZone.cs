using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerDeathHandler deathHandler =
            other.GetComponentInParent<PlayerDeathHandler>();

        if (deathHandler != null)
        {
            deathHandler.ShowDefeat();
        }
    }
}
