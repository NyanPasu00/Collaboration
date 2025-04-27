using UnityEngine;
using UnityEngine.SceneManagement; // very important

public class StartGame : MonoBehaviour
{
    public void LoadPlayBallScene()
    {
        SceneManager.LoadScene("Play Ball"); // <- your scene name here!
    }
}
