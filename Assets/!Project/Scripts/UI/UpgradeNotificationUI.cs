using UnityEngine;
using TMPro;
using System.Collections;

public class UpgradeNotificationUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TMP_Text upgradeText;

    [Header("Settings")]
    [SerializeField] private float displayDuration = 3f;

    private Coroutine notificationCoroutine;

    private void Awake()
    {
        notificationPanel.SetActive(false);
    }

    public void ShowUpgrade(string message, Color color)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        upgradeText.text = message;
        upgradeText.color = color;

        notificationPanel.SetActive(true);

        notificationCoroutine =
            StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(displayDuration);

        notificationPanel.SetActive(false);

        notificationCoroutine = null;
    }
}