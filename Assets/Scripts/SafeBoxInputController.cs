using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafeBoxInputController : MonoBehaviour
{
    public Animator safeAnimator;
    public TMP_Text displayText;
    public TMP_Text messageText;
    public GameObject keypadCanvas;
    private string enteredCode = "";
    private string correctCode = "907";

    void Start()
    {
        keypadCanvas.SetActive(false); // Ensure the keypad is hidden initially
    }

    public void AddDigit(string digit)
    {
        if (enteredCode.Length < 3)
        {
            enteredCode += digit;
            UpdateDisplay();
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateDisplay();
        messageText.text = "";
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            messageText.text = "Correct!";
            OpenSafe();
            keypadCanvas.SetActive(false); // Hide the keypad canvas
        }
        else
        {
            messageText.text = "Incorrect!";
            ClearCode();
        }
    }

    public void ExitKeypad()
    {
        keypadCanvas.SetActive(false); // Hide the keypad canvas
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for player movement
        Cursor.visible = false; // Hide the cursor
    }

    private void UpdateDisplay()
    {
        displayText.text = enteredCode;
    }

    private void OpenSafe()
    {
        if (safeAnimator != null)
        {
            safeAnimator.SetBool("Open", true);

            // Lock cursor back for player movement
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
