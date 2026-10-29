using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float rayHeight = 0.5f;

    private void Update()
    {
        Vector3 origin = transform.position + Vector3.up * rayHeight;

        // Dirección de la cámara, pero horizontal
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0f;
        direction.Normalize();

        Debug.DrawRay(origin, direction * interactDistance, Color.yellow);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit, interactDistance))
            {
                CraftingStation station = hit.collider.GetComponent<CraftingStation>();
                if (station != null)
                {
                    station.Use();
                }
            }
        }
    }
}
