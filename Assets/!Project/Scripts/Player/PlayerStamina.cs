
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float regenerationRate = 20f;
    [SerializeField] private float regenerationDelay = 1f;

    private float currentStamina;
    private float regenerationTimer;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (regenerationTimer > 0f)
        {
            regenerationTimer -= Time.deltaTime;
            return;
        }

        if (currentStamina < maxStamina)
        {
            currentStamina += regenerationRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    public bool HasStamina(float amount)
    {
        return currentStamina >= amount;
    }

    public bool ConsumeStamina(float amount)
    {
        if (!HasStamina(amount))
            return false;

        currentStamina -= amount;
        regenerationTimer = regenerationDelay;

        return true;
    }
    public void IncreaseMaxStamina(float amount)
    {
        if (amount <= 0f)
            return;

        maxStamina += amount;

        currentStamina = Mathf.Min(
            currentStamina + amount,
            maxStamina
        );
    }
}
