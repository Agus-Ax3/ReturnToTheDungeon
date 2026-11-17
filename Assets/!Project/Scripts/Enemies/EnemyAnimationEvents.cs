using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;

    public void EndAttack()
    {
        enemyAI.EndAttack();
    }
}