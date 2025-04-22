using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Controller : MonoBehaviour
{
    void Start()
    {
        // Randomly pick a cause of death (0–4)
        int randomCause = Random.Range(0, 5); // 0 = Hunger, 1 = Sadness, etc.
        PlayerPrefs.SetInt("CauseOfDeath", randomCause);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("2Heartfelt");
        }
    }
}
