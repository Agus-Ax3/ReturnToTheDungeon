using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = playerHealth.MaxHealth;
        healthSlider.value = playerHealth.CurrentHealth;
    }

    private void Update()
    {
        healthSlider.maxValue = playerHealth.MaxHealth;
        healthSlider.value = playerHealth.CurrentHealth;
    }
}