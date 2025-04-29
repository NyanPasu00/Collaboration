using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class SongCategoryButton : MonoBehaviour
{
    public string categoryName; // e.g., "Rock", "Rap"

    public Button lightToggleButton;

    public GameObject SongMenuPanel;
    public GameObject HallPanel;
    public GameObject MusicScreenPanel;

    public Animator petAnimator; // Reference to character's Animator
    public Transform petPosition;
    public AudioSource musicPlayer; // Audio Source to play the song
    public TextMeshProUGUI reactionText;
    public Energy_Bar happinessBar; 
    private Coroutine regenHappinessCoroutine;

    public AudioClip rockMusic;
    public AudioClip rapMusic;
    public AudioClip reggaeMusic;
    public AudioClip countryMusic;
    public AudioClip jazzMusic;
    public AudioClip metalMusic;

    public void ChangeToMusicScreen()
    {
        SongMenuPanel.SetActive(false);
        HallPanel.SetActive(false);
        MusicScreenPanel.SetActive(true);
        lightToggleButton.gameObject.SetActive(false);

        // Play music
        PlayCategoryMusic();

        // Play animation based on category
        if (IsLikedCategory(categoryName))
        {
            petAnimator.SetBool("Sad", false);
            petAnimator.SetBool("Laydown", false);
            petAnimator.SetBool("Dance", true);
            petPosition.localPosition = new Vector3(-0.41f, -3.93f, 0f);
            reactionText.text = "I Love this song!";

            if (regenHappinessCoroutine == null)
            {
                Debug.Log("Regen!!!");
                regenHappinessCoroutine = StartCoroutine(RegenerateHappiness());
            }
            happinessBar.PauseHappinessDeduction();
            
        }
        else
        {
            petAnimator.SetBool("Dance", false);
            petAnimator.SetBool("Laydown", false);
            petAnimator.SetBool("Sad", true);
            petPosition.localPosition = new Vector3(-1.97f, -3.81f, 0f);
            reactionText.text = "I hate this song...";

            
            if (regenHappinessCoroutine != null)
            {
                StopCoroutine(regenHappinessCoroutine);
                regenHappinessCoroutine = null;
            }

            happinessBar.ResumeHappinessDeduction();
        }
    }

    private bool IsLikedCategory(string category)
    {
        return category == "Rock" || category == "Country" || category == "Jazz";
    }

    private void PlayCategoryMusic()
    {
        switch (categoryName)
        {
            case "Rock":
                musicPlayer.clip = rockMusic;
                break;
            case "Rap":
                musicPlayer.clip = rapMusic;
                break;
            case "Reggae":
                musicPlayer.clip = reggaeMusic;
                break;
            case "Country":
                musicPlayer.clip = countryMusic;
                break;
            case "Jazz":
                musicPlayer.clip = jazzMusic;
                break;
            case "Metal":
                musicPlayer.clip = metalMusic;
                break;
            default:
                musicPlayer.clip = null;
                break;
        }

        if (musicPlayer.clip != null)
            musicPlayer.Play();
    }

    public void ChangeBackToHall()
    {
        MusicScreenPanel.SetActive(false);
        SongMenuPanel.SetActive(false);
        HallPanel.SetActive(true);
        lightToggleButton.gameObject.SetActive(true);
        // Stop the music
        if (musicPlayer.isPlaying)
            musicPlayer.Stop();

        // Reset animation states
        petAnimator.SetBool("Dance", false);
        petAnimator.SetBool("Sad", false);
        petAnimator.SetBool("Laydown", true);
        petPosition.localPosition = new Vector3(-2.09765f, -2.680398f, 0f);

        if (regenHappinessCoroutine != null)
        {
            StopCoroutine(regenHappinessCoroutine);
            regenHappinessCoroutine = null;
        }

        happinessBar.ResumeHappinessDeduction();
    }
    private IEnumerator RegenerateHappiness()
    {
        while (happinessBar.happiness_current < happinessBar.happiness_max)
        {
            happinessBar.happiness_current += 1;
            if (happinessBar.happiness_current > happinessBar.happiness_max)
                happinessBar.happiness_current = happinessBar.happiness_max;

            happinessBar.happiness_Slider.value = (float)happinessBar.happiness_current / happinessBar.happiness_max;
            happinessBar.happinessDetail_Slider.value = (float)happinessBar.happiness_current / happinessBar.happiness_max;

            yield return new WaitForSeconds(1f); // Adjust delay as needed
        }

        regenHappinessCoroutine = null; // Reset reference after fully regenerated
    }
}