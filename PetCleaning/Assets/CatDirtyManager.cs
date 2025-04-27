using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CatDirtyManager : MonoBehaviour
{
    public GameObject dirtySpotPrefab;   // Assign a prefab in the Inspector
    public GameObject cat;               // Reference to the cat GameObject
    public GameObject cloud;
    public TMP_Text messageText;
    public float maxDirty = 100;
    public float dirty = 0;
    public bool hasUsedSoap = false;
    private bool hasShownHalfCleanMessage = false;
    public bool firstTime = true;
    private float dirtyTimer = 0f;
    public float dirtyInterval = 50f; // seconds
    private float lastMilestone = 0;
    public ShowerController currentShower;
    public Animator catAnimator;

    void Start()
    {
        CheckMilestoneAndSpawn();
    }

    void CheckMilestoneAndSpawn()
    {
        if (dirty == 100)
        {
            lastMilestone = 100;
            SpawnDirtySpots(10);
            cloud.SetActive(true);
        }
        else if (dirty >= 80)
        {
            lastMilestone = 80;
            SpawnDirtySpots(8);
        }
        else if (dirty >= 60)
        {
            lastMilestone = 60;
            SpawnDirtySpots(6);
        }
        else if (dirty >= 40)
        {
            lastMilestone = 40;
            SpawnDirtySpots(4);
        }
        else if (dirty >= 20)
        {
            lastMilestone = 20;
            SpawnDirtySpots(2);
        }
    }



    void Update()
    {
        dirtyTimer += Time.deltaTime;

        if (dirtyTimer >= dirtyInterval)
        {
            dirtyTimer = 0f;
            IncreaseDirt();
        }

        if (dirty <= 0 && currentShower != null)
        {
            currentShower.StopShower();
            currentShower = null;

            hasUsedSoap = false;
            hasShownHalfCleanMessage = false; // Reset for future use
            firstTime = true;

            if (catAnimator != null)
            {
                catAnimator.SetBool("isClean", true);
                cat.transform.position = new Vector3(0f, 0f, 0f);
                Debug.Log("clean cat");

                // 🆕 After 2 seconds, set isClean back to false
                Invoke(nameof(ResetToIdle), 2f);
            }
                
            ShowCloudMessage("All clean! Great job!", 2.5f);  // Clean message

        }
    }

    void ResetToIdle()
    {
        if (catAnimator != null)
        {
            catAnimator.SetBool("isClean", false);
        }
    }

    void IncreaseDirt()
    {
        if (dirty < maxDirty)
        {
            dirty++;
            Debug.Log("Dirt increased: " + dirty);

            if (dirty % 20 == 0 && dirty != lastMilestone)
            {
                lastMilestone = dirty;
                SpawnDirtySpots(2);
            }

            //if (dirty >= maxDirty)
            //{
            //    cloud.SetActive(true);
            //    Debug.Log("The cat needs a bath!");
            //}
            if (dirty >= maxDirty)
            {
                ShowCloudMessage("I need to bath !!! :(", 2.5f);
                Debug.Log("The cat needs a bath!");
            }

        }
    }

    void SpawnDirtySpots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject spot = Instantiate(dirtySpotPrefab);

            // Set parent to cat so the spots are attached to it
            spot.transform.SetParent(cat.transform);

            // Random local position on the cat (adjust the range as needed)
            Vector3 randomPos = new Vector3(
                Random.Range(-0.1f, 0.01f),
                Random.Range(-0.13f, 0.08f),
                Random.Range(-0.02f, 0.02f)
            );
            spot.transform.localPosition = randomPos;

            // Random scale to make spots different sizes
            float randomScale = Random.Range(0.01f, 0.03f);
            spot.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            Debug.Log("Spawned dirty spot at: " + randomPos);
        }
    }

    public void ShowCloudMessage(string msg, float duration = 2f)
    {
        if (cloud != null)
            cloud.SetActive(true);

        if (messageText != null)
            messageText.text = msg;

        CancelInvoke("HideCloudMessage");
        Invoke(nameof(HideCloudMessage), duration);
    }

    public void HideCloudMessage()
    {
        if (cloud != null)
            cloud.SetActive(false);
        hasShownHalfCleanMessage = true;
    }

    public void DecreaseDirtGradually(float amount)
    {
        if (dirty > 0)
        {
            dirty -= amount;
            dirty = Mathf.Clamp(dirty, 0, maxDirty); // Prevent going below 0
            Debug.Log("Dirt decreased to: " + dirty);
        }
    }

    // Handle the soap being used (called from SoapBubbleSpawner)
    public void OnSoapUsed()
    {
        hasUsedSoap = true; // Set soap flag to true
        hasShownHalfCleanMessage = false; // Reset for future use

        if (firstTime == true && dirty >= maxDirty)
        {
            // Hide the message because soap is being used
            HideCloudMessage();
            firstTime = false;
        }
    }
}
