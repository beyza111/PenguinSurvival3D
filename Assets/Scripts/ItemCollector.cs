using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private bool hasHammer = false;
    private bool hasRedFish = false;
    private int blueFishCount = 0; // Track the number of blue fish collected
    public Camera mainCamera; // Reference to the main camera
    public Camera thirdPersonCamera; // Reference to the third-person camera
    public GameObject redFishPrefab; // Prefab for the red fish
    public GameObject blueFishPrefab; // Prefab for the blue fish
    public Transform birdHouse; // Transform of the bird house
    private GameObject collectedRedFishPrefab; // Reference to the collected red fish prefab
    public static bool fishDropped = false; // Flag to track if the fish has been dropped
    public HungerBar hungerBar; // Reference to the HungerBar script
    public GameObject signGameObject; // Reference to the sign GameObject

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            CollectHammer(other.gameObject);
        }
        else if (other.CompareTag("BirdHouse") && hasRedFish)
        {
            DropRedFishAtBirdHouse();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForFishClick();
        }
    }

    private void CollectHammer(GameObject hammer)
    {
        hasHammer = true;
        Destroy(hammer);
        Debug.Log("Hammer collected! Now you can collect fishes.");
    }

    private void CollectRedFish(GameObject redFish)
    {
        collectedRedFishPrefab = redFishPrefab;
        hasRedFish = true;
        Destroy(redFish);
        Debug.Log("Red fish collected!");
    }

    private void CollectBlueFish(GameObject blueFish)
    {
        blueFishCount++; // Increment the count of blue fish collected
        Destroy(blueFish);
        Debug.Log("Blue fish collected!");

        // Update hunger bar
        if (hungerBar != null)
        {
            hungerBar.IncreaseFishCount();
        }
    }

    private void CheckForFishClick()
    {
        if (!hasHammer)
        {
            Debug.Log("You need to collect the hammer first!");
            return;
        }

        Camera activeCamera = GetActiveCamera();
        if (activeCamera == null)
        {
            Debug.LogError("No active camera found!");
            return;
        }

        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("RedFish") && !hasRedFish)
            {
                CollectRedFish(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("BlueFish"))
            {
                CollectBlueFish(hit.collider.gameObject);
            }
        }
    }

    private Camera GetActiveCamera()
    {
        if (mainCamera != null && mainCamera.isActiveAndEnabled)
        {
            return mainCamera;
        }
        else if (thirdPersonCamera != null && thirdPersonCamera.isActiveAndEnabled)
        {
            return thirdPersonCamera;
        }
        else
        {
            Debug.LogError("No active camera found!");
            return null;
        }
    }

    private void DropRedFishAtBirdHouse()
    {
        if (collectedRedFishPrefab != null)
        {
            GameObject droppedFish = Instantiate(collectedRedFishPrefab, birdHouse.position, birdHouse.rotation);
            droppedFish.SetActive(true); // Ensure the instantiated red fish is active
            hasRedFish = false; // Reset the red fish collection status
            fishDropped = true; // Set the flag indicating the fish has been dropped
            Debug.Log("Red fish dropped at the bird house!");

            // Activate sign game object
            if (signGameObject != null)
            {
                signGameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("No red fish prefab to instantiate!");
        }
    }
}
