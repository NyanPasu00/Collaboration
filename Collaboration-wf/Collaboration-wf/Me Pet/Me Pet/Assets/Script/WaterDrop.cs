//using UnityEngine;

//public class WaterDrop : MonoBehaviour
//{
//    public GameObject showerController; // Prefab with shower head + particle system + sound

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Pet"))
//        {
//            Debug.Log("Water drop touched the pet, transforming into shower!");

//            Vector3 showerPosition = other.transform.position + new Vector3(0, 2.3f, 0);
//            GameObject shower = Instantiate(showerController, showerPosition, Quaternion.identity);

//            // Assign the pet as the target in case it's not pre-set
//            ShowerController controller = shower.GetComponent<ShowerController>();
//            if (controller != null && controller.petTarget == null)
//            {
//                controller.petTarget = other.transform;
//                //controller.StartShower();
//            }

//            Destroy(gameObject);
//        }
//    }
//}

using UnityEngine;
using TMPro;

public class WaterDrop : MonoBehaviour
{
    public GameObject showerController;
    public Transform waterDropStartPos;
    public TMP_Text messageText;
    public GameObject cloud;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pet"))
        {
            CatDirtyManager catManager = FindAnyObjectByType<CatDirtyManager>();
            if (catManager != null)
            {
                // Block shower if not dirty enough
                if (catManager.dirty < catManager.maxDirty)
                {
                    catManager.ShowCloudMessage("I still not so dirty yet >_<", 2f);
                    return;
                }

                if (!catManager.hasUsedSoap) // Only show message if soap not used
                {
                    catManager.ShowCloudMessage("Oops! I look so dirty. Try using soap!", 2f);
                    return;
                }

                // ✅ Soap was used — allow shower
                Debug.Log("Water drop touched the pet, transforming into shower!");

                Vector3 showerPosition = other.transform.position + new Vector3(0, 2.3f, 0);
                GameObject shower = Instantiate(showerController, showerPosition, Quaternion.identity);

                ShowerController controller = shower.GetComponent<ShowerController>();
                if (controller != null)
                {
                    controller.petTarget = other.transform;
                    controller.waterDrop = this.gameObject;
                    controller.waterDropStartPos = waterDropStartPos;
                    controller.StartShower();
                }

                catManager.currentShower = controller;
                gameObject.SetActive(false); // Hide instead of destroy
            }
        }
    }

    void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }
}

