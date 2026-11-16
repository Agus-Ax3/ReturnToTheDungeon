using UnityEngine;

public class CombatAnimationEvents : MonoBehaviour
{
    [SerializeField] private SwordHitbox swordHitbox;

    public void EnableSwordHitbox(float damage)
    {
        swordHitbox.EnableHitbox(damage);
    }

    public void DisableSwordHitbox()
    {
        swordHitbox.DisableHitbox();
    }
}