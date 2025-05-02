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

    private void Start()
    {
        energy = FindFirstObjectByType<Energy_Bar>();
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
        energy = FindFirstObjectByType<Energy_Bar>();
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

        yield return null;

        // Wait for Energy_Bar to be findable
        yield return new WaitUntil(() => FindFirstObjectByType<Energy_Bar>() != null);

        energy = FindFirstObjectByType<Energy_Bar>();
    }

    // Optional: Scene-specific wrappers for buttons
    public void LoadPetNameScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.newPetData();
        PlayAndLoad("PetNameScene");
    }

    public void NewToHallScene()
    {
        PlayAndLoad("HallScene");
    }

    public void BackToHallScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        PlayAndLoad("HallScene");
    }

    public void BackToKitchenScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        PlayAndLoad("KitchenScene");
    }

    public void BackToMedicationScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        PlayAndLoad("MedicationScene");
    }

    public void BackToBathRoomScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        PlayAndLoad("BathRoomScene");
    }

    public void BackToGameRoomScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
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