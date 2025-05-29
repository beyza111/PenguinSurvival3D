using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider hungerSlider;
    private int fishCount = 0;
    private const int maxFishCount = 10; // Maximum fish count that can fill the hunger bar

    void Start()
    {
        UpdateHungerBar();
    }

    // Call this method when the player collects a blue fish
    public void IncreaseFishCount()
    {
        fishCount++;
        if (fishCount > maxFishCount)
        {
            fishCount = maxFishCount;
        }
        UpdateHungerBar();
    }

    // Update the hunger bar UI based on the current fish count
    private void UpdateHungerBar()
    {
        float hungerValue = (float)fishCount / maxFishCount;
        hungerSlider.value = hungerValue;
    }
}
