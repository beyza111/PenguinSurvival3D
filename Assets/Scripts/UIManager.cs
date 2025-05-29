using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text messageText; // Reference to the UI Text component

    private void Start()
    {
        ClearMessage();
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }

    public void ClearMessage()
    {
        messageText.text = "";
    }
}
