using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpened;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController fpsController = other.GetComponent<FPSController>();
            if (fpsController != null && fpsController.hasKey)
            {
                
                doorClosed.SetActive(false);
                doorOpened.SetActive(true);
            }
        }
    }
}
