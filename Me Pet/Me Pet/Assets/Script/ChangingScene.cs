using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHallScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("PetNameScene");
    }

    public void BackToHallScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("HallScene");
    }

    public void BackToKitchenScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("KitchenScene");
    }

    public void BackToMedicationScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("MedicationScene");
    }

    public void BackToBathRoomScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("BathRoomScene");
    }

    public void BackToGameRoomScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("GameRoomScene");
    }
}
