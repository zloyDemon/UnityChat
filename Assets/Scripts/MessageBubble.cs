using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] Image bubbleBG;

    [Header("Bubble's background sprites")]
    [SerializeField] Sprite ownerBG;
    [SerializeField] Sprite otherBG;

    public void SetMessageData(string messageString, string date, bool isOwner)
    {
        messageText.text = messageString;
        dateText.text = date;
        bubbleBG.sprite = isOwner ? ownerBG : otherBG;
    }
}
