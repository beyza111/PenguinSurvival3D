using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class IglooZoneManager : MonoBehaviour
{
    public GameObject igloo; // Reference to the igloo GameObject (assign in inspector)
    public ParticleSystem snowStorm; // Reference to the ParticleSystem for the snowstorm (assign in inspector)
    public Transform checkpoint; // Reference to the checkpoint Transform (assign in inspector)
    public UIManager uiManager; // Reference to the UIManager (assign in inspector)

    private bool playerInIgloo = false;
    private bool stormActive = false;
    private bool stormHappened = false; // Track if the storm has already happened
    private float stormDuration = 10f; // Duration of the storm in seconds
    private Coroutine stormCoroutine;
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Assuming the player has the tag "Player"
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject has the tag 'Player'.");
        }

        if (uiManager == null)
        {
            Debug.LogError("UIManager not assigned! Please assign the UIManager in the inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stormActive && !stormHappened)
        {
            stormCoroutine = StartCoroutine(StartStorm());
            Debug.Log("Player entered igloo zone. Starting storm...");
            uiManager.ShowMessage("Firtina basladi igloya kos!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerInIgloo && stormActive)
        {
            Debug.Log("You cannot leave the igloo during the storm!");
            player.transform.position = igloo.transform.position;
        }
    }

    private IEnumerator StartStorm()
    {
        stormActive = true;
        stormHappened = true; // Ensure the storm happens only once
        snowStorm.Play();
        Debug.Log("Snowstorm started! Get inside the igloo!");

        yield return new WaitForSeconds(stormDuration);

        if (!playerInIgloo)
        {
            Debug.Log("Player did not reach the igloo in time! Respawning at checkpoint...");
            player.transform.position = checkpoint.position;
        }

        snowStorm.Stop();
        stormActive = false;
        playerInIgloo = false;
        uiManager.ClearMessage();
        Debug.Log("Snowstorm ended! You can leave the igloo now.");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && stormActive && !playerInIgloo)
        {
            if (Vector3.Distance(other.transform.position, igloo.transform.position) < 1f)
            {
                playerInIgloo = true;
                Debug.Log("Player is inside the igloo.");
                uiManager.ClearMessage();
            }
        }
    }

    void Update()
    {
        if (stormActive && !playerInIgloo)
        {
            // Check if the player is outside the igloo during the storm
            if (Vector3.Distance(player.transform.position, igloo.transform.position) > 2f)
            {
                Debug.Log("Player is outside the igloo during the storm!");
                // Player will be respawned at the end of the storm if not in igloo
            }
        }
    }
}
