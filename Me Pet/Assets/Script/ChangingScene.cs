using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public AudioSource audioClip; // click sound
    public Energy_Bar energy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioClip = GetComponent<AudioSource>();
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject); // destroy old one
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioClip = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Reconnect Energy_Bar after scene changes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        energy = FindObjectOfType<Energy_Bar>();
    }

    // Public function to use in button OnClick
    public void PlayAndLoad(string sceneName)
    {
        if (audioClip != null)
        {
            StartCoroutine(PlaySoundAndLoad(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName); // fallback
        }
    }

    private System.Collections.IEnumerator PlaySoundAndLoad(string sceneName)
    {
        audioClip.Play();
        yield return new WaitForSeconds(0.4f); // wait short delay (or audioClip.clip.length)
        SceneManager.LoadScene(sceneName);
    }

    // Optional: Scene-specific wrappers for buttons
    public void LoadPetNameScene()
    {
        energy?.newPetData();
        PlayAndLoad("PetNameScene");
    }

    public void BackToHallScene()
    {
        energy?.SavePetData();
        PlayAndLoad("HallScene");
    }

    public void BackToKitchenScene()
    {
        energy?.SavePetData();
        PlayAndLoad("KitchenScene");
    }

    public void BackToMedicationScene()
    {
        energy?.SavePetData();
        PlayAndLoad("MedicationScene");
    }

    public void BackToBathRoomScene()
    {
        energy?.SavePetData();
        PlayAndLoad("BathRoomScene");
    }

    public void BackToGameRoomScene()
    {
        energy?.SavePetData();
        PlayAndLoad("GameRoomScene");
    }

    public void playAudio()
    {
        if (audioClip != null)
        {
            audioClip.Play();
        }
    }

}