using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBoxController : MonoBehaviour
{
    private Animator animator;
    private Camera mainCamera;
    public GameObject keypadCanvas; // Reference to the keypad canvas

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on SafeBoxController object.");
        }

        if (mainCamera == null)
        {
            Debug.LogError("MainCamera object not found or does not have a Camera component.");
        }

        if (keypadCanvas == null)
        {
            Debug.LogError("KeypadCanvas object not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && animator != null && mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                ShowKeypad();
            }
        }
    }

    public void ShowKeypad()
    {
        if (keypadCanvas != null)
        {
            keypadCanvas.SetActive(true); // Activate the keypad canvas

            // Unlock cursor for UI interaction
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OpenSafe()
    {
        if (animator != null)
        {
            animator.SetBool("Open", true);
        }
    }
}
