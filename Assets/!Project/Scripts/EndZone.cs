using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndZone : MonoBehaviour
{
    private bool reached = false;

    private void Update()
    {
        if (reached && Keyboard.current.rKey.wasPressedThisFrame)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            reached = true;
            Debug.Log("Fin del prototipo");
        }
    }

    private void OnGUI()
    {
        if (reached)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height),
                      "Fin del prototipo\n\nPresioná R para reiniciar", style);
        }
    }
}