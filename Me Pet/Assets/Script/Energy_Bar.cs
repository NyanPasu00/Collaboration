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

        StartCoroutine(DeductEnergyOverTime());
        StartCoroutine(DeductHungerOverTime());
        StartCoroutine(DeductHappinessOverTime());
        StartCoroutine(DeductHealthOverTime());
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
}