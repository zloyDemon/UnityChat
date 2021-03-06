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
    private VerticalLayoutGroup rootVerticalLayoutGroup;
    private VerticalLayoutGroup bubbleVerticalLayoutGroup;
    private LayoutElement layoutElement;

    public enum MessageBubbleType
    {
        Usual,
        Last,
    }

    private void Awake()
    {
        rootVerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        bubbleVerticalLayoutGroup = bubbleBG.gameObject.GetComponent<VerticalLayoutGroup>();
        layoutElement = bubbleBG.GetComponent<LayoutElement>();
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
        var bubbleBgSprite = isOwner ? (isLast ? ownerBGLastMessage : ownerBG) : isLast ? otherBGLastMessage : otherBG;
        bubbleBG.sprite = bubbleBgSprite;
        messageAuthor.gameObject.SetActive(isLast);

        if (isOwner)
        {
            dateText.rectTransform.anchoredPosition = new Vector2(isLast ? -45 : -15, dateText.rectTransform.anchoredPosition.y);
            rootVerticalLayoutGroup.padding.right = isLast ? 0 : 30;
            bubbleVerticalLayoutGroup.padding.right = isLast ? 50 : 20;
            layoutElement.minWidth = isLast ? 130 : 100;
        }
        else
        {
            rootVerticalLayoutGroup.padding.left = isLast ? 0 : 30;
            bubbleVerticalLayoutGroup.padding.left = isLast ? 50 : 20;
            layoutElement.minWidth = isLast ? 130 : 100;
        }
    }
}
