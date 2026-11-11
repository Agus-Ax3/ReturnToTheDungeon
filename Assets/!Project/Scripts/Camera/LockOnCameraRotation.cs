
using UnityEngine;
using Unity.Cinemachine;

public class LockOnCameraRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LockOnSystem lockOnSystem;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;

    [Header("Camera Settings")]
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float verticalAngle = 17.5f;

    private void LateUpdate()
    {
        if (player == null ||
            lockOnSystem == null ||
            orbitalFollow == null)
            return;

        if (!lockOnSystem.IsLockedOn)
            return;

        Vector3 direction =
            lockOnSystem.CurrentTarget.position -
            player.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        // Ángulo horizontal hacia el enemigo
        float targetAngle = Mathf.Atan2(
            direction.x,
            direction.z
        ) * Mathf.Rad2Deg;

        // Interpolar el ángulo para evitar giros bruscos
        float currentAngle =
            orbitalFollow.HorizontalAxis.Value;

        orbitalFollow.HorizontalAxis.Value =
            Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );

        // Mantener la inclinación vertical
        orbitalFollow.VerticalAxis.Value =
            verticalAngle;
    }
}
