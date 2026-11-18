using UnityEngine;
using Unity.Cinemachine;

public class Portal : MonoBehaviour
{
    private RoomManager roomManager;

    private void Start()
    {
        roomManager = FindFirstObjectByType<RoomManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CharacterController controller =
            other.GetComponent<CharacterController>();

        if (controller == null || roomManager == null)
            return;

        Transform destination = roomManager.GetNextDestination();

        if (destination == null) return;

        // Desactivar el LockOn
        LockOnSystem lockOnSystem =
            other.GetComponent<LockOnSystem>();

        if (lockOnSystem != null)
            lockOnSystem.ClearTarget();

        // Obtener las cámaras del PlayerSystem
        Transform player = other.transform;

        Transform playerSystem = player.parent != null
            ? player.parent
            : player;

        CinemachineCamera[] cameras =
            playerSystem.GetComponentsInChildren<CinemachineCamera>(true);

        Vector3 oldPosition = player.position;

        // Guardar las posiciones de los objetivos de cámara
        Vector3[] oldTargetPositions = new Vector3[cameras.Length];

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].Follow != null)
                oldTargetPositions[i] = cameras[i].Follow.position;
        }

        // Teletransportar al jugador
        controller.enabled = false;

        player.SetPositionAndRotation(
            destination.position,
            destination.rotation
        );

        controller.enabled = true;

        // Actualizar el objetivo de la cámara LockOn
        LockOnFocusController focus =
            playerSystem.GetComponentInChildren<LockOnFocusController>(true);

        if (focus != null)
            focus.transform.position = player.position + Vector3.up * 1.5f;

        // Actualizar las cámaras Cinemachine
        for (int i = 0; i < cameras.Length; i++)
        {
            Transform target = cameras[i].Follow;

            if (target != null)
            {
                Vector3 delta =
                    target.position - oldTargetPositions[i];

                cameras[i].OnTargetObjectWarped(target, delta);
            }
            else
            {
                cameras[i].OnTargetObjectWarped(
                    player,
                    player.position - oldPosition
                );
            }
        }
    }
}
