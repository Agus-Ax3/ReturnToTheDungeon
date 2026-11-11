
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnSystem : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float lockOnRange = 15f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Camera mainCamera;

    [Header("Input")]
    [SerializeField] private InputActionReference lockOnAction;

    public Transform CurrentTarget { get; private set; }
    public bool IsLockedOn => CurrentTarget != null;

    private void OnEnable()
    {
        if (lockOnAction != null)
            lockOnAction.action.Enable();
    }

    private void OnDisable()
    {
        if (lockOnAction != null)
            lockOnAction.action.Disable();
    }

    private void Update()
    {
        if (lockOnAction != null &&
            lockOnAction.action.WasPressedThisFrame())
        {
            Debug.Log("se presione q");
            if (IsLockedOn)
                ClearTarget();
            else
                FindTarget();
        }

        if (IsLockedOn)
        {
            float distance = Vector3.Distance(
                transform.position,
                CurrentTarget.position
            );

            if (distance > lockOnRange)
                ClearTarget();
        }
    }


    private void FindTarget()
    {
        if (mainCamera == null)
        {
            Debug.LogError("No hay Main Camera asignada");
            return;
        }

        Collider[] enemies = Physics.OverlapSphere(
            transform.position,
            lockOnRange,
            enemyLayer
        );

        Debug.Log("Enemigos detectados: " + enemies.Length);

        float bestScore = float.MaxValue;
        Transform bestTarget = null;

        foreach (Collider enemy in enemies)
        {
            Debug.Log("Collider encontrado: " + enemy.name);

            LockOnTarget target =
                enemy.GetComponentInChildren<LockOnTarget>();

            if (target == null)
            {
                Debug.LogWarning(
                    "No tiene LockOnTarget: " + enemy.name
                );
                continue;
            }

            Vector3 viewportPoint =
                mainCamera.WorldToViewportPoint(
                    target.TargetPoint.position
                );

            Debug.Log("Posición en pantalla: " + viewportPoint);

            if (viewportPoint.z <= 0)
                continue;

            if (viewportPoint.x < 0 || viewportPoint.x > 1 ||
                viewportPoint.y < 0 || viewportPoint.y > 1)
                continue;

            Vector2 screenCenter = new Vector2(0.5f, 0.5f);

            float score = Vector2.Distance(
                screenCenter,
                new Vector2(viewportPoint.x, viewportPoint.y)
            );

            if (score < bestScore)
            {
                bestScore = score;
                bestTarget = target.TargetPoint;
            }
        }

        CurrentTarget = bestTarget;

        if (CurrentTarget != null)
            Debug.Log("LOCK-ON ACTIVADO: " + CurrentTarget.name);
        else
            Debug.LogWarning("No se encontró un objetivo válido");
    }

    public void ClearTarget()
    {
        CurrentTarget = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position,
            lockOnRange
        );
    }
}
