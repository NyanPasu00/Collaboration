using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Energy_Bar : MonoBehaviour
{
    [SerializeField]
    [Header("Energy")]
    public int energy_max = 100;
    public int energy_current;
    public Slider energy_Slider;
    public float energy_deduct_time = 60f;

    [Header("Hunger")]
    public int hunger_max = 100;
    public int hunger_current;
    public Slider hunger_Slider;
    public float hunger_deduct_time = 60f;

    [Header("Happiness")]
    public int happiness_max = 100;
    public int happiness_current;
    public Slider happiness_Slider;
    public float happiness_deduct_time = 60f;

    [Header("Health")]
    public int health_max = 100;
    public int health_current;
    public Slider health_Slider;
    public float health_deduct_time = 60f;

    [Header("Progress")]
    public int progress_max = 100;
    public int progress_current;
    public Image progress_Image;
    public float progress_increase_time = 60f;

    public enum PetStage
    {
        Kid,
        Teen,
        Adult,
        Old
    }

    public PetStage currentStage = PetStage.Kid;

    void Start()
    {
        energy_current = energy_max; // Initialize energy
        energy_Slider.value = 1; // Full energy at start

        hunger_current = hunger_max; // Initialize hunger
        hunger_Slider.value = 1; // Full hunger at start

        happiness_current = happiness_max; // Initialize happiness
        happiness_Slider.value = 1; // Full happiness at start

        health_current = health_max; // Initialize health
        health_Slider.value = 1; // Full health at start

        progress_current = 0;
        progress_Image.fillAmount = 0;

        StartCoroutine(DeductEnergyOverTime());
        StartCoroutine(DeductHungerOverTime());
        StartCoroutine(DeductHappinessOverTime());
        StartCoroutine(DeductHealthOverTime());
        StartCoroutine(IncreaseProgressOverTime());
    }

    IEnumerator DeductEnergyOverTime()
    {
        while (energy_current > 0)
        {
            yield return new WaitForSeconds(energy_deduct_time); // Wait for 1 minute
            DeductEnergy(1); // Reduce 1% of max energy
        }
    }

    IEnumerator DeductHungerOverTime()
    {
        while (energy_current > 0)
        {
            yield return new WaitForSeconds(energy_deduct_time); // Wait for 1 minute
            DeductHunger(1); // Reduce 1% of max energy
        }
    }

    IEnumerator DeductHappinessOverTime()
    {
        while (energy_current > 0)
        {
            yield return new WaitForSeconds(energy_deduct_time); // Wait for 1 minute
            DeductHappiness(1); // Reduce 1% of max energy
        }
    }

    IEnumerator DeductHealthOverTime()
    {
        while (energy_current > 0)
        {
            yield return new WaitForSeconds(energy_deduct_time); // Wait for 1 minute
            DeductHealth(1); // Reduce 1% of max energy
        }
    }

    IEnumerator IncreaseProgressOverTime()
    {
        while (progress_current < progress_max)
        {
            yield return new WaitForSeconds(progress_increase_time); // Wait 60 seconds
            AddProgress(1); // Add 1% each time
        }
    }

    void DeductEnergy(int percent)
    {
        int amountToDeduct = Mathf.CeilToInt((percent / 100f) * energy_max);
        energy_current = Mathf.Max(0, energy_current - amountToDeduct);
        GetEnergyFill();
    }

    void DeductHunger(int percent)
    {
        int amountToDeduct = Mathf.CeilToInt((percent / 100f) * hunger_max);
        hunger_current = Mathf.Max(0, hunger_current - amountToDeduct);
        GetHungerFill();
    }

    void DeductHappiness(int percent)
    {
        int amountToDeduct = Mathf.CeilToInt((percent / 100f) * happiness_max);
        happiness_current = Mathf.Max(0, happiness_current - amountToDeduct);
        GetHappinessFill();
    }

    void DeductHealth(int percent)
    {
        int amountToDeduct = Mathf.CeilToInt((percent / 100f) * health_max);
        health_current = Mathf.Max(0, health_current - amountToDeduct);
        GetHealthFill();
    }

    void GetEnergyFill()
    {
        energy_Slider.value = (float)energy_current / energy_max;
    }
    void GetHungerFill()
    {
        hunger_Slider.value = (float)hunger_current / hunger_max;
    }
    void GetHappinessFill()
    {
        happiness_Slider.value = (float)happiness_current / happiness_max;
    }
    void GetHealthFill()
    {
        health_Slider.value = (float)health_current / health_max;
    }

    void AddProgress(int percent)
    {
        int amountToAdd = Mathf.CeilToInt((percent / 100f) * progress_max);
        progress_current = Mathf.Min(progress_max, progress_current + amountToAdd);
        GetProgressFill();
    }

    void GetProgressFill()
    {
        progress_Image.fillAmount = (float)progress_current / progress_max;
    }


    public void IncreaseProgress(int value)
    {
        progress_current += value;

        if (progress_current >= progress_max)
        {
            progress_current = 0; // Reset progress
            AdvanceStage();
        }
    }

    void AdvanceStage()
    {
        if (currentStage == PetStage.Kid)
            currentStage = PetStage.Teen;
        else if (currentStage == PetStage.Teen)
            currentStage = PetStage.Adult;
        else if (currentStage == PetStage.Adult)
            currentStage = PetStage.Old;
        // If already Old, you can decide whether to do nothing or show "Passed Away"
    }
}