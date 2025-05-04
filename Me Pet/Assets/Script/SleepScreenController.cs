using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LightToggle : MonoBehaviour
{
    public Button musicToggleButton;
    public GameObject HallLightScreen;
    public GameObject HallDarkScreen;
    public Animator petAnimator;  // Reference to the pet's Animator
    public Energy_Bar energyBar; // Drag your Energy_Bar GameObject here in the Inspector
    private Coroutine regenEnergyCoroutine;
    private bool isLightOn = true;
    public bool isSleeping = true;

    public AudioSource audio;

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

            if (regenEnergyCoroutine != null)
            {
                StopCoroutine(regenEnergyCoroutine);
                regenEnergyCoroutine = null;
            }
            isSleeping = false;
            PlayerPrefs.SetInt("IsSleeping", isSleeping ? 1 : 0);
            PlayerPrefs.Save();
            energyBar.ResumeEnergyDeduction();
        }
        else
        {
            // Light is OFF: Pet should sleep
            petAnimator.SetBool("Laydown", false);
            petAnimator.SetBool("Sleep", true);
            musicToggleButton.gameObject.SetActive(false);

            if (regenEnergyCoroutine == null)
            {
                regenEnergyCoroutine = StartCoroutine(RegenerateEnergy());
            }
            isSleeping = true;
            PlayerPrefs.SetInt("IsSleeping", isSleeping ? 1 : 0);
            PlayerPrefs.Save();
            energyBar.PauseEnergyDeduction();
        }


    }

    private IEnumerator RegenerateEnergy()
    {
        while (energyBar.energy_current < energyBar.energy_max)
        {
            energyBar.energy_current += 1;
            if (energyBar.energy_current > energyBar.energy_max)
                energyBar.energy_current = energyBar.energy_max;

            energyBar.energy_Slider.value = (float)energyBar.energy_current / energyBar.energy_max;
            energyBar.energyDetail_Slider.value = (float)energyBar.energy_current / energyBar.energy_max;

            yield return new WaitForSeconds(2f); // Adjust delay as needed
        }

        regenEnergyCoroutine = null; // Reset reference after fully regenerated
    }

    public void playAudio()
    {
        audio.Play();
    }

}
