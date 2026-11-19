using UnityEngine;
using System.Collections.Generic;

public class PlayerUpgrades : MonoBehaviour
{
    public enum UpgradeType
    {
        Health,
        Damage,
        Stamina
    }

    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private SwordHitbox swordHitbox;
    [SerializeField] private UpgradeNotificationUI notificationUI;

    [Header("Upgrade Settings")]
    [SerializeField] private float healthBonus = 25f;
    [SerializeField] private float damageBonus = 20f;
    [SerializeField] private float staminaBonus = 25f;

    private List<UpgradeType> availableUpgrades =
        new List<UpgradeType>()
        {
            UpgradeType.Health,
            UpgradeType.Damage,
            UpgradeType.Stamina
        };

    public bool ApplyRandomUpgrade()
    {
        if (availableUpgrades.Count == 0)
            return false;

        int index = Random.Range(0, availableUpgrades.Count);

        UpgradeType upgrade = availableUpgrades[index];

        switch (upgrade)
        {
            case UpgradeType.Health:

                playerHealth.IncreaseMaxHealth(healthBonus);

                notificationUI?.ShowUpgrade(
                    $"Maximum Health +{healthBonus}",
                    new Color32(214, 91, 91, 255)
                );

                Debug.Log("Mejora obtenida: Vida");
                break;

            case UpgradeType.Damage:

                swordHitbox.IncreaseDamage(damageBonus);

                notificationUI?.ShowUpgrade(
                    $"Attack Damage +{damageBonus}%",
                    new Color32(212, 155, 53, 255)
                );

                Debug.Log("Mejora obtenida: Daño");
                break;

            case UpgradeType.Stamina:

                playerStamina.IncreaseMaxStamina(staminaBonus);

                notificationUI?.ShowUpgrade(
                    $"Maximum Stamina +{staminaBonus}",
                    new Color32(105, 184, 121, 255)
                );

                Debug.Log("Mejora obtenida: Stamina");
                break;
        }

        availableUpgrades.RemoveAt(index);

        return true;
    }
}