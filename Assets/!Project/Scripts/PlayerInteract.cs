using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float rayHeight = 1f;
    [SerializeField] private float downTilt = 0.5f;

    private void Update()
    {
        Vector3 origin = transform.position + Vector3.up * rayHeight;
        Vector3 direction = (transform.forward + Vector3.down * downTilt).normalized;

        Debug.DrawRay(origin, direction * interactDistance, Color.yellow);

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Apretaste E");

            if (Physics.Raycast(origin, direction, out RaycastHit hit, interactDistance))
            {
                Debug.Log("El rayo tocó: " + hit.collider.name);

                
            }
            else
            {
                Debug.Log("El rayo no tocó nada");
            }
        }
    }
}
