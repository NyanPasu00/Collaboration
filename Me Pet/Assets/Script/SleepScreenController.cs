using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public GameObject HallLightScreen;
    public GameObject HallDarkScreen;

    private bool isLightOn = true;

    public void ToggleLight()
    {
        isLightOn = !isLightOn;

        HallLightScreen.SetActive(isLightOn);
        HallDarkScreen.SetActive(!isLightOn);
    }
}

