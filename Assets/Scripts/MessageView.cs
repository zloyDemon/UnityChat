using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    public void SetText(string text)
    {
        messageText.text = text;
    }
}
