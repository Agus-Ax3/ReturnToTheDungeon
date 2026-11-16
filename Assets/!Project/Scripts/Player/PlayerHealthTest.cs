using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthTest : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            playerHealth.TakeDamage(20f);
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            playerHealth.Heal(20f);
        }
    }
}