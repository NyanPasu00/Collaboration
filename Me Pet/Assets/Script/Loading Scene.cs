using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class LoadingScene : MonoBehaviour
{

    public bool petDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        petDead = PlayerPrefs.GetInt("PetDead", 0) == 1;

        if (petDead == true)
        {
            SceneManager.LoadScene("StartScene");
        }
        else if(petDead == false)
        {
            if(FindFirstObjectByType<Energy_Bar>().currentStage == Energy_Bar.PetStage.Kid)
            {
                SceneManager.LoadScene("HallScene");
            }
            else if (FindFirstObjectByType<Energy_Bar>().currentStage == Energy_Bar.PetStage.Teen)
            {
                SceneManager.LoadScene("TeenHallScene");
            }
            else if (FindFirstObjectByType<Energy_Bar>().currentStage == Energy_Bar.PetStage.Adult)
            {
                SceneManager.LoadScene("AdultHallScene");
            }
            else if (FindFirstObjectByType<Energy_Bar>().currentStage == Energy_Bar.PetStage.Old)
            {
                SceneManager.LoadScene("OldHallScene");
            }
        }
    }

    
}
