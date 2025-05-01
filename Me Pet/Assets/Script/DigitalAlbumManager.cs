using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DigitalAlbumManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject EmptyDigitalAlbum;
    public GameObject PetList;
    public GameObject PetAlbum;

    [Header("Containers")]
    public RectTransform petListContent;
    public RectTransform stagesContent;

    [Header("Prefabs")]
    public GameObject petItemPrefab;
    public GameObject stageItemPrefab;

    [Header("UI")]
    public TextMeshProUGUI petAlbumTitle;
    public TextMeshProUGUI gratitudeText;

    private List<PetData> allPetList = new List<PetData>();
    private Dictionary<string, PetAlbumData> petAlbumDataDict = new Dictionary<string, PetAlbumData>();
    private bool isAlbumOpen = false;

    private readonly string[] gratitudeMessages = new string[]
    {
        "From my first steps to my last breath, your love was everything to me.",
        "You made my short life feel full. Now, live yours with the same love you gave me.",
        "You took care of me with love. Now, take care of yourself the same way—gently, patiently, and every day.",
        "Every time you comforted me, you were practicing how to heal your own heart.",
        "You gave me peace in every moment. Now, promise to seek that peace for yourself.",
        "I grew because you cared. Now it’s your turn—grow for yourself."
    };

    void Start()
    {
        allPetList = new List<PetData>
        {
            new PetData { name = "Luna", stagePassed = "Teen" },
            new PetData { name = "Mochi", stagePassed = "Old" }
        };

        //Caches randomly selected images once per pet, so it doesn't change each time
        foreach (var pet in allPetList)
        {
            if (!petAlbumDataDict.ContainsKey(pet.name))
                CacheRandomImagesForPet(pet.name, pet.stagePassed);
        }
    }

    public void ToggleDigitalAlbum()
    {
        isAlbumOpen = !isAlbumOpen;
        gameObject.SetActive(isAlbumOpen);

        if (isAlbumOpen) ShowDigitalAlbum();
    }

    public void ShowDigitalAlbum()
    {
        PetList.SetActive(true);
        PetAlbum.SetActive(false);

        EmptyDigitalAlbum.SetActive(allPetList.Count == 0);
        PetList.SetActive(allPetList.Count > 0);

        PopulatePetList();
    }

    void PopulatePetList()
    {

        foreach (Transform child in petListContent)
            Destroy(child.gameObject);

        foreach (var pet in allPetList)
        {
            //custom UI layout showing a pet's image and name
            GameObject petGO = Instantiate(petItemPrefab, petListContent);

            //Sets the displayed text to the pet's name
            petGO.transform.Find("PetNameText").GetComponent<TextMeshProUGUI>().text = pet.name;

            //pre-chosen image names for Kid/Teen/Adult/Old stages
            PetAlbumData albumData = petAlbumDataDict[pet.name];

            //Chooses the most appropriate available stage image for preview
            string stageFolder = GetPreviewStage(albumData, out string imageName);

            //Loads the image
            Sprite sprite = Resources.Load<Sprite>($"DigitalAlbum/{stageFolder}/{imageName}");

            //If the sprite was loaded, sets it to the Image UI component named PetImage inside the prefab
            if (sprite != null)
                petGO.transform.Find("PetImage").GetComponent<Image>().sprite = sprite;

            //switches to the album panel and shows that pet's stage images
            petGO.GetComponent<Button>().onClick.AddListener(() => ShowPetAlbum(pet.name));
        }
    }

    public void ShowPetAlbum(string petName)
    {

        PetList.SetActive(false);
        PetAlbum.SetActive(true);

        // Display pet name
        if (petAlbumTitle != null)
            petAlbumTitle.text = petName;

        // Checks if the selected pet already has saved (cached) stage image selections
        if (!petAlbumDataDict.ContainsKey(petName))
        {
            //Finds the full PetData object from allPetList by matching the name
            var pet = allPetList.Find(p => p.name == petName);
            if (pet != null)
            {
                CacheRandomImagesForPet(pet.name, pet.stagePassed);
            }
            else
            {
                Debug.LogError($"Pet not found in list: {petName}");
                return;
            }
        }

        // Remove any previously displayed images from stagesContent
        foreach (Transform child in stagesContent)
            Destroy(child.gameObject);

        // Retrieve the pet’s saved image names
        PetAlbumData data = petAlbumDataDict[petName];

        AddStageImageIfExists("KidStage", data.kidImageName);
        AddStageImageIfExists("TeenStage", data.teenImageName);
        AddStageImageIfExists("AdultStage", data.adultImageName);
        AddStageImageIfExists("OldStage", data.oldImageName);

        if (gratitudeText != null)
        {
            int index = data.gratitudeIndex;
            if (index >= 0 && index < gratitudeMessages.Length)
                gratitudeText.text = gratitudeMessages[index];
            else
                gratitudeText.text = "";
        }
    }

    void AddStageImageIfExists(string folder, string imageName)
    {
        if (string.IsNullOrEmpty(imageName))
        {
            Debug.LogWarning($"Skipped loading empty image from {folder}");
            return;
        }

        Sprite sprite = Resources.Load<Sprite>($"DigitalAlbum/{folder}/{imageName}");
        if (sprite == null)
        {
            Debug.LogError($"Sprite not found: DigitalAlbum/{folder}/{imageName}");
            return;
        }

        Debug.Log($"Loaded: DigitalAlbum/{folder}/{imageName}");

        GameObject item = Instantiate(stageItemPrefab, stagesContent);
        Image img = item.GetComponent<Image>();
        if (img != null)
            img.sprite = sprite;
        else
            Debug.LogError("StageItemPrefab has no Image component!");
    }



    void TryLoadAndDisplay(string stageFolder, string imageName)
    {

        if (!stageItemPrefab.activeSelf)
            Debug.LogWarning("stageItemPrefab is disabled!");

        if (string.IsNullOrEmpty(imageName))
        {
            Debug.LogWarning($"Skipped loading empty image from {stageFolder}");
            return;
        }

        string path = $"DigitalAlbum/{stageFolder}/{imageName}";
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.LogError($"Sprite not found at: {path}");
            return;
        }

        GameObject stageGO = Instantiate(stageItemPrefab, stagesContent);
        Image img = stageGO.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("StageItemPrefab has NO Image component!");
        }
        else
        {
            img.sprite = sprite;
            Debug.Log($"Sprite set to: {sprite.name}");
        }


        Debug.Log($"Loaded: {path}");

        Debug.Log($"Instantiated stage item for {stageFolder}/{imageName}");

    }




    void DisplayStageImage(string folder, string imageName)
    {
        if (!stageItemPrefab.activeSelf)
            Debug.LogWarning("stageItemPrefab is disabled!");

        if (string.IsNullOrEmpty(imageName)) return;

        Sprite sprite = Resources.Load<Sprite>($"DigitalAlbum/{folder}/{imageName}");
        if (sprite == null) return;

        GameObject go = Instantiate(stageItemPrefab, stagesContent);
        go.GetComponent<Image>().sprite = sprite;
    }

    //Selects 1 image per eligible stage and stores in dictionary
    void CacheRandomImagesForPet(string petName, string stagePassed)
    {
        PetAlbumData data = new PetAlbumData(petName);
        data.kidImageName = GetRandomImageName("KidStage");

        if (stagePassed == "Teen" || stagePassed == "Adult" || stagePassed == "Old")
            data.teenImageName = GetRandomImageName("TeenStage");

        if (stagePassed == "Adult" || stagePassed == "Old")
            data.adultImageName = GetRandomImageName("AdultStage");

        if (stagePassed == "Old")
            data.oldImageName = GetRandomImageName("OldStage");

        data.gratitudeIndex = Random.Range(0, gratitudeMessages.Length);

        petAlbumDataDict[petName] = data;
    }

    string GetPreviewStage(PetAlbumData data, out string imageName)
    {
        if (!string.IsNullOrEmpty(data.oldImageName)) { imageName = data.oldImageName; return "OldStage"; }
        if (!string.IsNullOrEmpty(data.adultImageName)) { imageName = data.adultImageName; return "AdultStage"; }
        if (!string.IsNullOrEmpty(data.teenImageName)) { imageName = data.teenImageName; return "TeenStage"; }
        imageName = data.kidImageName; return "KidStage";
    }

    string GetRandomImageName(string folder)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>($"DigitalAlbum/{folder}");
        if (sprites.Length == 0) return null;
        return sprites[Random.Range(0, sprites.Length)].name;
    }
}

[System.Serializable]
public class PetData
{
    public string name;
    public string stagePassed;
}

[System.Serializable]
public class PetAlbumData
{
    public string petName;
    public string kidImageName;
    public string teenImageName;
    public string adultImageName;
    public string oldImageName;
    public int gratitudeIndex = -1;

    public PetAlbumData(string name) { petName = name; }
}
