using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject exitPortal;

    private bool completed = false;

    private void Start()
    {
        if (exitPortal != null)
        {
            exitPortal.SetActive(false);
        }
    }
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
    public void Complete()
    {
        if (completed) return;
        completed = true;

        if (exitPortal != null)
        {
            exitPortal.SetActive(true);
        }

        Debug.Log(name + " completada");
    }
}
