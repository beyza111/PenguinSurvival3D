using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public GameObject dialoguePanel; // UI Panel to show the message and options
    public Text birdMessageText; // Text element to display the bird's message
    public Button option1Button; // Button for the first response option
    public Button option2Button; // Button for the second response option
    public Button option3Button; // Button for the third response option

    public Button option1ButtonNew; // New button for option 1
    public Button option2ButtonNew; // New button for option 2
    public Button option3ButtonNew; // New button for option 3

    private Animator animator;
    private FPSController fpsController;

    void Start()
    {
        // Get the Animator component and set the bird to idle animation
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Hide dialogue panel initially
        }

        // Disable the new buttons initially
        option1ButtonNew.gameObject.SetActive(false);
        option2ButtonNew.gameObject.SetActive(false);
        option3ButtonNew.gameObject.SetActive(false);

        // Assign methods to button click events
        option1Button.onClick.AddListener(() => HandleOptionSelected(1));
        option2Button.onClick.AddListener(() => HandleOptionSelected(2));
        option3Button.onClick.AddListener(() => HandleOptionSelected(3));

        // Assign methods to new button click events
        option1ButtonNew.onClick.AddListener(HideDialoguePanel);
        option2ButtonNew.onClick.AddListener(HideDialoguePanel);
        option3ButtonNew.onClick.AddListener(HideDialoguePanel);

        // Find the FPSController in the scene
        fpsController = FindObjectOfType<FPSController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !ItemCollector.fishDropped)
        {
            // Re-enable the original buttons when the player enters the trigger area
            option1Button.gameObject.SetActive(true);
            option2Button.gameObject.SetActive(true);
            option3Button.gameObject.SetActive(true);

            ShowMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideMessage();
        }
    }

    private void ShowMessage()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            birdMessageText.text = "Merhaba! Ben Frosty. Nasil yardimci olabilirim?"; // Initial bird message

            // Unlock cursor for UI interaction
            if (fpsController != null)
            {
                fpsController.UnlockCursor();
            }
        }
        else
        {
            Debug.Log("Dialogue panel is not set.");
        }
    }

    private void HideMessage()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);

            // Lock cursor back for player movement
            if (fpsController != null)
            {
                fpsController.LockCursor();
            }
        }
    }

    private void HandleOptionSelected(int optionNumber)
    {
        // Hide the old buttons
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        option3Button.gameObject.SetActive(false);

        // Display the new buttons based on the option selected
        switch (optionNumber)
        {
            case 1:
                birdMessageText.text = "Uzgunum, Onlari gormedim.";
                option1ButtonNew.gameObject.SetActive(true);
                break;
            case 2:
                birdMessageText.text = "Gorusuruz yine!";
                option2ButtonNew.gameObject.SetActive(true);
                break;
            case 3:
                birdMessageText.text = "Önce lezziz kirmizi baligi evime birakmalisin.";
                option3ButtonNew.gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid option selected.");
                break;
        }
    }

    private void HideDialoguePanel()
    {
        // Hide the dialogue panel
        dialoguePanel.SetActive(false);

        // Lock cursor back for player movement
        if (fpsController != null)
        {
            fpsController.LockCursor();
        }
    }
}

