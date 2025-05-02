using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public static BGMScript Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.Play(); // Auto-play when loaded
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ChangeClip(AudioClip newClip, bool playImmediately = true)
    {
        audioSource.clip = newClip;
        if (playImmediately)
        {
            PlayMusic();
        }
    }
}
