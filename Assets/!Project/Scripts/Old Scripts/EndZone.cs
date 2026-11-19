using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject victoryPanel;

    [Header("Scene Settings")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    private bool reached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (reached || !other.CompareTag("Player"))
            return;

        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null || playerHealth.IsDead)
            return;

        reached = true;

        MouseController mouseController =
            playerHealth.GetComponent<MouseController>();

        if (mouseController != null)
            mouseController.enabled = false;

        victoryPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Victoria: Dungeon completada");
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }
}