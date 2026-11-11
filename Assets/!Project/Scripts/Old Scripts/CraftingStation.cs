using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private float messageDuration = 3f;

    private float hideMessageTime = 0f;

    public void Use()
    {
        Debug.Log("Usaste la estación de crafteo");
        hideMessageTime = Time.time + messageDuration;
    }

    private void OnGUI()
    {
        if (Time.time < hideMessageTime)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 28;
            GUI.Label(new Rect(20, 60, 800, 50), "Estación de crafteo: próximamente podrás forjar mejoras", style);
        }
    }
}
