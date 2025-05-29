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
               
                doorClosed.SetActive(false);
                doorOpened.SetActive(true);

                
                if (key != null)
                {
                    key.SetActive(false);
                }
            }
        }
    }
}


