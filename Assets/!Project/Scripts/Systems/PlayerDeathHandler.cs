using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Defeat UI")]
    [SerializeField] private GameObject defeatPanel;

    [SerializeField] private string mainMenuScene = "MainMenu";

    private PlayerHealth playerHealth;
    private LockOnSystem lockOnSystem;
    private MouseController mouseController;

    private bool defeated = false;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        lockOnSystem = GetComponent<LockOnSystem>();
        mouseController = GetComponent<MouseController>();

        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerHealth != null && playerHealth.IsDead)
        {
            ShowDefeat();
        }
    }

    public void ShowDefeat()
    {
        if (defeated)
            return;

        defeated = true;

        if (lockOnSystem != null)
            lockOnSystem.ClearTarget();

        if (mouseController != null)
            mouseController.enabled = false;

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }
}
