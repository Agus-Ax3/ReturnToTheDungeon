using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthTest : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        if (Keyboard.current == null || playerHealth == null)
            return;

        
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            Vector3 attackerPosition =
                playerHealth.transform.position +
                playerHealth.transform.forward * 2f;

            playerHealth.TakeDamage(20f, attackerPosition);
        }

        
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            playerHealth.Heal(20f);
        }

        
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Vector3 attackerPosition =
                playerHealth.transform.position -
                playerHealth.transform.forward * 2f;

            playerHealth.TakeDamage(20f, attackerPosition);
        }
    }
}