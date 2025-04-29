using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class EventTrigger : MonoBehaviour
{
    public string categoryFriendName;

    public TextMeshProUGUI lonelyText;
    public GameObject lonelyPanel;
    public GameObject ChooseFriendPanel;
    public Animator petAnimator;
    void Start()
    {
        string petName = PlayerPrefs.GetString("PetName", "Pet");
        lonelyText.text = $"{petName}'s Felt Lonely is time to find him a friend";
    }
    public void FriendEventTrigger()
    {
       lonelyPanel.SetActive(false);
       ChooseFriendPanel.SetActive(true);
    }

    public void ChooseAFriend()
    {
        if (IsLikedFriend(categoryFriendName))
        {
            ChooseFriendPanel.SetActive(false);
            petAnimator.SetBool("Laydown", false);
            petAnimator.SetBool("Excited",true);

        }
        else
        {
            ChooseFriendPanel.SetActive(false);
            petAnimator.SetBool("Sad", true);

        }
    }

    private bool IsLikedFriend(string category)
    {
        return category == "Cobays" || category == "Potato" || category == "Jade" || category == "Coffee";
    }
}