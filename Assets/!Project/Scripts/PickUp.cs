using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{

    [SerializeField] private string itemName = "Goblin Ear";
    [SerializeField] private bool completesRoom = false;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Collect()
    {
        Debug.Log("Recogiste: " + itemName);

        if (completesRoom)
        {
            Room room = GetComponentInParent<Room>();
            if (room != null)
            {
                room.Complete();
            }
        }

        Destroy(gameObject);
    }

    private void OnGUI()
    {
        if (playerInRange)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 28;
            GUI.Label(new Rect(20, 20, 600, 50), "Presioná E para recoger " + itemName, style);
        }
    }
}
