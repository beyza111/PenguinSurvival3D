using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<string> inventory = new HashSet<string>();

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log("Added " + itemName + " to inventory.");
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log("Removed " + itemName + " from inventory.");
        }
        else
        {
            Debug.Log("Item " + itemName + " not found in inventory.");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}
