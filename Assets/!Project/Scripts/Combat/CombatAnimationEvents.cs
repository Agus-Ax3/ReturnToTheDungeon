using UnityEngine;

public class CombatAnimationEvents : MonoBehaviour
{
    [SerializeField] private SwordHitbox swordHitbox;

    public void EnableSwordHitbox()
    {
        swordHitbox.EnableHitbox();
    }

    public void DisableSwordHitbox()
    {
        swordHitbox.DisableHitbox();
    }
}