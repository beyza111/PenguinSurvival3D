using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;
    public GameObject key;

    void Start()
    {
        // Ensure the door is initially closed
        doorClosed.SetActive(true);
        doorOpened.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController fpsController = other.GetComponent<FPSController>();
            if (fpsController != null && fpsController.hasKey)
            {
                // Open the door if the player has the key
                doorClosed.SetActive(false);
                doorOpened.SetActive(true);

                // Optionally disable the key object as it's been used to open the door
                if (key != null)
                {
                    key.SetActive(false);
                }
            }
        }
    }
}


