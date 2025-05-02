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
        FindFirstObjectByType<Energy_Bar>()?.newPetData();
        PlayAndLoad("PetNameScene");
    }

    public void BackToHallScene()
    {

        FindFirstObjectByType<Energy_Bar>()?.SavePetData();

        if (energy.currentStage == Energy_Bar.PetStage.Kid)
        { 
            PlayAndLoad("HallScene");
        }else if (energy.currentStage == Energy_Bar.PetStage.Teen)
        {
            PlayAndLoad("TeenHallScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Adult)
        {
            PlayAndLoad("AdultHallScene");
        }
        else if(energy.currentStage == Energy_Bar.PetStage.Old)
        {
            PlayAndLoad("OldHallScene");
        }
    }

    public void BackToKitchenScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        if (energy.currentStage == Energy_Bar.PetStage.Kid)
        {
            PlayAndLoad("KitchenScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Teen)
        {
            PlayAndLoad("TeenKitchenScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Adult)
        {
            PlayAndLoad("AdultKitchenScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Old)
        {
            PlayAndLoad("OldKitchenScene");
        }
    }

    public void BackToMedicationScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        PlayAndLoad("MedicationScene");
    }

    public void BackToBathRoomScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();

        if (energy.currentStage == Energy_Bar.PetStage.Kid)
        {
            PlayAndLoad("BathRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Teen)
        {
            PlayAndLoad("TeenBathRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Adult)
        {
            PlayAndLoad("AdultBathRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Old)
        {
            PlayAndLoad("OldBathRoomScene");
        }
    }

    public void BackToGameRoomScene()
    {
        FindFirstObjectByType<Energy_Bar>()?.SavePetData();
        if (energy.currentStage == Energy_Bar.PetStage.Kid)
        {
            PlayAndLoad("GameRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Teen)
        {
            PlayAndLoad("TeenGameRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Adult)
        {
            PlayAndLoad("AdultGameRoomScene");
        }
        else if (energy.currentStage == Energy_Bar.PetStage.Old)
        {
            PlayAndLoad("OldGameRoomScene");
        }
    }

    public void playAudio()
    {
        if (audioClip != null)
        {
            audioClip.Play();
        }
    }

}