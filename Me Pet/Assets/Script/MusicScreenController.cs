using UnityEngine;

public class SongCategoryButton : MonoBehaviour
{
    public GameObject SongMenuPanel;    // Reference to the Song Menu Panel
    public GameObject MusicScreenPanel; // Reference to the Music Panel (Music screen)

    public void ChangeToMusicScreen()
    {
        // Hide the Song Menu Panel
        SongMenuPanel.SetActive(false);

        // Show the Music Panel (music screen)
        MusicScreenPanel.SetActive(true);

        // Optional: Set up the music to play depending on the chosen category
        // For example, you can call a method here to play the selected song.
        // PlayMusic("Rock");
    }
}
