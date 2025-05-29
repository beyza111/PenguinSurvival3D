using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController fpsController = other.GetComponent<FPSController>();
            if (fpsController != null)
            {
                fpsController.hasKey = true;
                Debug.Log("Key collected");
                Destroy(gameObject); // Remove the key from the scene
            }
        }
    }
}
