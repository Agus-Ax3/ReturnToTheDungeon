using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    
        private void Start()
        {
            LockMouse();
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                UnlockMouse();
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                LockMouse();
            }
        }

        private void LockMouse()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockMouse()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
   
}


