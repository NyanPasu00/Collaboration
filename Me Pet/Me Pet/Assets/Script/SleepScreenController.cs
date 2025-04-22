using UnityEngine;
using UnityEngine.UI;

public class LightToggle : MonoBehaviour
{
    public Button musicToggleButton;
    public GameObject HallLightScreen;
    public GameObject HallDarkScreen;
    public Animator petAnimator;  // Reference to the pet's Animator
    
    private bool isLightOn = true;

    public void ToggleLight()
    {
        isLightOn = !isLightOn;

        HallLightScreen.SetActive(isLightOn);
        HallDarkScreen.SetActive(!isLightOn);

        if (isLightOn)
        {
            // Light is ON: Pet should lay down (awake but relaxed)
            petAnimator.SetBool("Laydown", true);
            petAnimator.SetBool("Sleep", false);
            musicToggleButton.gameObject.SetActive(true);
        }
        else
        {
            // Light is OFF: Pet should sleep
            petAnimator.SetBool("Laydown", false);
            petAnimator.SetBool("Sleep", true);
            musicToggleButton.gameObject.SetActive(false);
        }
    }
}
