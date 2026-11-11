using UnityEngine;
using Unity.Cinemachine;

public class Portal : MonoBehaviour
{
    private RoomManager roomManager;
    private CinemachineCamera playerCamera;

    private void Start()
    {
        roomManager = FindFirstObjectByType<RoomManager>();
        playerCamera = FindFirstObjectByType<CinemachineCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Transform destination = roomManager.GetNextDestination();
        CharacterController controller = other.GetComponent<CharacterController>();
        Vector3 delta = destination.position - other.transform.position;

        controller.enabled = false;
        other.transform.position = destination.position;
        other.transform.rotation = destination.rotation;
        controller.enabled = true;

        playerCamera.OnTargetObjectWarped(other.transform, delta);
    }
}
