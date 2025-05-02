using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EvolutionManager : MonoBehaviour
{
    public Animator petAnimator;
    public GameObject kidPet;      // The child pet GameObject
    public GameObject teenPet;     // The teen pet GameObject (initially hidden)
    public GameObject backButton;  // Button to go back to game (initially hidden
    public int loopCount = 3;

    void Start()
    {
        backButton.SetActive(false);
        teenPet.SetActive(false);
        StartCoroutine(PlayEvolutionAnimation());
    }

    IEnumerator PlayEvolutionAnimation()
    {
        for (int i = 0; i < loopCount; i++)
        {
            petAnimator.SetTrigger("StartEvolution");
            // Wait until animation finishes (replace 1.5f with your animation length)
            yield return new WaitForSeconds(1.5f);
        }

        // Switch to teen pet
        kidPet.SetActive(false);
        teenPet.SetActive(true);

        // Show back to game button
        backButton.SetActive(true);
    }

    public void BackToGame()
    {
        //FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        SceneManager.LoadScene("TeenHallScene");
    }
}
