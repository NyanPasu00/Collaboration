using UnityEngine;
using UnityEngine.UI;

public class FridgeClick : MonoBehaviour
{
    public GameObject FoodSelection;
    public Image selectedFoodImage; // add this reference

    void OnMouseDown()
    {
        if (FoodSelection != null)
        {
            FoodSelection.SetActive(true);

            // Hide bottom food image
            if (selectedFoodImage != null)
            {
                selectedFoodImage.color = new Color(1, 1, 1, 0); // make transparent
            }
        }
    }
}
