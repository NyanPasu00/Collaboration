using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Energy_Bar energy;

    public void LoadHallScene()
    {
        Debug.Log("Button clicked!");
        energy.firstTimePlay = true;
        SceneManager.LoadScene("PetNameScene");
    }

    public void BackToHallScene()
    {
        Debug.Log("Button clicked!");
        energy.SavePetData();
        SceneManager.LoadScene("HallScene");
        
    }

    public void BackToKitchenScene()
    {
        Debug.Log("Button clicked!");
        energy.SavePetData();
        SceneManager.LoadScene("KitchenScene");
    }

    public void BackToMedicationScene()
    {
        Debug.Log("Button clicked!");
        energy.SavePetData();
        SceneManager.LoadScene("MedicationScene");

    }

    public void BackToBathRoomScene()
    {
        Debug.Log("Button clicked!");
        energy.SavePetData();
        SceneManager.LoadScene("BathRoomScene");
    }

    public void BackToGameRoomScene()
    {
        Debug.Log("Button clicked!");
        energy.SavePetData();
        SceneManager.LoadScene("GameRoomScene");
    }
}
