using UnityEngine;
using UnityEngine.SceneManagement; // very important

public class StartGame : MonoBehaviour
{
    public void LoadPlayBallScene()
    {
        SceneManager.LoadScene("PlayBallScene"); // <- your scene name here!
    }
}
