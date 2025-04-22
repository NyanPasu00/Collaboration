using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHallScene()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("HallScene");
    }
}
