using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StatusDetail: MonoBehaviour
{
    public GameObject statusPanel;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI statusTitle;

    // These should reference your values from the other script
    public Energy_Bar stats;

    private bool panelOpen = true;

    void Update()
    {
        // Only update if panel is open
        if (panelOpen)
        {
            UpdateStatusText();

            // Hide if clicking outside
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                ClosePanel();
            }
        }
    }

    public void ToggleStatusPanel()
    {
        panelOpen = !panelOpen;
        statusPanel.SetActive(panelOpen);
        if (panelOpen)
        {
            UpdateStatusText();
        }
    }

    void ClosePanel()
    {
        statusPanel.SetActive(false);
        panelOpen = false;
    }

    void UpdateStatusText()
    {
        statusTitle.text = $"Lily's Status\n";
        statusText.text = $"Progress: {stats.progress_current}%\n" +
                          $"Energy: {stats.energy_current}%\n" +
                          $"Hunger: {stats.hunger_current}%\n" +
                          $"Happiness: {stats.happiness_current}%\n" +
                          $"Health: {stats.health_current}%";
    }

    bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
