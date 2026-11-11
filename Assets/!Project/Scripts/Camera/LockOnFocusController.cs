
using UnityEngine;

public class LockOnFocusController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LockOnSystem lockOnSystem;

    [Range(0f, 1f)]
    [SerializeField] private float focusWeight = 0.6f;

    [SerializeField] private float smoothSpeed = 8f;

    private void LateUpdate()
    {
        if (player == null || lockOnSystem == null)
            return;

        Vector3 targetPosition = player.position + Vector3.up * 1.5f;

        if (lockOnSystem.IsLockedOn)
        {
            targetPosition = Vector3.Lerp(
                targetPosition,
                lockOnSystem.CurrentTarget.position,
                focusWeight
            );
        }

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
