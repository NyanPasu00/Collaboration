using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoapBubbleSpawner : MonoBehaviour
{
    public GameObject bubble;
    public GameObject water;
    public Button leftButton;
    public Button rightButton;
    public float bubbleDelay = 0.7f;
    public int maxBubbles = 5;
    public float minDistanceBetweenBubbles = 20.5f;
    private bool hasShownHalfCleanMessage = false;

    private float lastBubbleTime = 0f;
    private List<GameObject> activeBubbles = new List<GameObject>();


    private void Start()
    {
        water.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pet"))
        {

            CatDirtyManager catManager = FindAnyObjectByType<CatDirtyManager>();

            if (catManager == null)
                return;

            // ❌ Show message if not dirty
            if (catManager.dirty < 20)
            {
                catManager.ShowCloudMessage("I still not so dirty yet >_<", 2f);
                return;
            }

            // ✅ Dirty enough: apply soap
            if (Time.time - lastBubbleTime > bubbleDelay && activeBubbles.Count < maxBubbles)
            {
                rightButton.interactable = false;
                leftButton.interactable = false;
                Vector3 spawnPos = other.ClosestPoint(transform.position);

                bool tooClose = false;
                foreach (GameObject bubbleObj in activeBubbles)
                {
                    if (bubbleObj != null && Vector3.Distance(bubbleObj.transform.position, spawnPos) < minDistanceBetweenBubbles)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    GameObject newBubble = Instantiate(bubble, spawnPos, Quaternion.identity);
                    activeBubbles.Add(newBubble);
                    lastBubbleTime = Time.time;

                    //catManager.hasUsedSoap = true;
                    //catManager.HideCloudMessage();
                    catManager.OnSoapUsed();

                }
                if (activeBubbles.Count >= maxBubbles)
                {
                    water.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
        activeBubbles.RemoveAll(b => b == null);
        CatDirtyManager catManager = FindAnyObjectByType<CatDirtyManager>();

        if (activeBubbles.Count > 5 && activeBubbles.Count != maxBubbles && !hasShownHalfCleanMessage)
        {
            catManager.ShowCloudMessage("Almost there! Keep scrubbing to make your pet shine!", 2.5f);
            Debug.Log("Message shown: Almost there!");
            
        }
    }
}
