
using UnityEngine;
using Unity.Cinemachine;

public class LockOnCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LockOnSystem lockOnSystem;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private CinemachineCamera lockOnCamera;

    private void LateUpdate()
    {
        if (lockOnSystem == null ||
            freeLookCamera == null ||
            lockOnCamera == null)
            return;

        bool locked = lockOnSystem.IsLockedOn;

        freeLookCamera.Priority = locked ? 0 : 10;
        lockOnCamera.Priority = locked ? 20 : 0;
    }
}

