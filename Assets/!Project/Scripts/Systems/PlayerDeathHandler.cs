
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] private float restartDelay = 3f;

    private PlayerHealth playerHealth;
    private LockOnSystem lockOnSystem;

    private bool restarting = false;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        lockOnSystem = GetComponent<LockOnSystem>();
    }

    private void Update()
    {
        if (restarting || playerHealth == null)
            return;

        if (playerHealth.IsDead)
        {
            restarting = true;

            if (lockOnSystem != null)
                lockOnSystem.ClearTarget();

            StartCoroutine(RestartRun());
        }
    }

    private IEnumerator RestartRun()
    {
        Debug.Log("Jugador derrotado. Reiniciando run...");

        yield return new WaitForSecondsRealtime(restartDelay);

        SceneManager.LoadScene(
            gameObject.scene.buildIndex
        );
    }
}
