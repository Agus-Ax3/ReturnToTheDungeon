
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private Slider staminaSlider;

    private void Start()
    {
        staminaSlider.maxValue = playerStamina.MaxStamina;
        staminaSlider.value = playerStamina.CurrentStamina;
    }

    private void Update()
    {
        staminaSlider.maxValue = playerStamina.MaxStamina;
        staminaSlider.value = playerStamina.CurrentStamina;
    }
}
