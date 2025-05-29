using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public GameObject bearStanding; // Reference to the standing bear GameObject
    public GameObject bearLying; // Reference to the lying bear GameObject

    void Start()
    {
        // Ensure the bear is initially standing
        bearStanding.SetActive(true);
        bearLying.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            // Switch to the lying bear when hit by a snowball
            bearStanding.SetActive(false);
            bearLying.SetActive(true);

            // Optionally destroy the snowball
            Destroy(collision.gameObject);
        }
    }
}
