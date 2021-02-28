using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubble : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI messageAuthor;
    [SerializeField] Image bubbleBG;

    [Header("Bubble's background sprites")]
    [SerializeField] Sprite ownerBG;
    [SerializeField] Sprite otherBG;
    [SerializeField] Sprite ownerBGLastMessage;
    [SerializeField] Sprite otherBGLastMessage;

    private Message currentMessage;
    private VerticalLayoutGroup verticalLayoutGroup;

    public enum MessageBubbleType
    {
        Usual,
        Last,
    }

    private void Awake()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    public void SetMessageData(Message message)
    {
        currentMessage = message;
        messageText.text = message.MessageText;
        dateText.text = DateUtils.GetDateNowFormat();
        bubbleBG.sprite = UChatApp.Instance.IsOwnerUser(message.Sender) ? ownerBG : otherBG;
        messageAuthor.text = message.Sender.Name;
    }

    public void TransformMessageBubble(MessageBubbleType type)
    {
        bool isOwner = UChatApp.Instance.IsOwnerUser(currentMessage.Sender);
        bool isLast = type == MessageBubbleType.Last;
        Sprite bubbleBGSprite;
        bubbleBGSprite = isOwner ? (isLast ? ownerBGLastMessage : ownerBG) : isLast ? otherBGLastMessage : otherBG;
        bubbleBG.sprite = bubbleBGSprite;
        messageAuthor.gameObject.SetActive(isLast);

        if (isOwner)
        {
            dateText.rectTransform.anchoredPosition = new Vector2(isLast ? -45 : -15, dateText.rectTransform.anchoredPosition.y);
            verticalLayoutGroup.padding.right = isLast ? 49 : 20;
        }
        else
        {
            verticalLayoutGroup.padding.left = isLast ? 49 : 20;
        }
    }
}
