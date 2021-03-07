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
        const int RootPaddingIsLast = 0;
        const int RootPadding = 30;
        const int BubblePaddingIsLast = 50;
        const int BubblePadding = 20;
        const int LayoutElementMinWidth = 100;
        const int LayoutElementMinWidthIsLast = 130;
        const float DateTextAnchoredPositionX = -15;
        const float DateTextAnchoredPositionXIsLast = -45;

        bool isOwner = UChatApp.Instance.IsOwnerUser(currentMessage.Sender);
        bool isLast = type == MessageBubbleType.Last;
        var bubbleBgSprite = isOwner ? (isLast ? ownerBGLastMessage : ownerBG) : isLast ? otherBGLastMessage : otherBG;
        bubbleBG.sprite = bubbleBgSprite;
        messageAuthor.gameObject.SetActive(isLast);

        if (isOwner)
        {
            dateText.rectTransform.anchoredPosition = new Vector2(isLast ? DateTextAnchoredPositionXIsLast : DateTextAnchoredPositionX, dateText.rectTransform.anchoredPosition.y);
            rootVerticalLayoutGroup.padding.right = isLast ? RootPaddingIsLast : RootPadding;
            bubbleVerticalLayoutGroup.padding.right = isLast ? BubblePaddingIsLast : BubblePadding;
            layoutElement.minWidth = isLast ? LayoutElementMinWidthIsLast : LayoutElementMinWidth;
        }
        else
        {
            rootVerticalLayoutGroup.padding.left = isLast ? RootPaddingIsLast : RootPadding;
            bubbleVerticalLayoutGroup.padding.left = isLast ? BubblePaddingIsLast : BubblePadding;
            layoutElement.minWidth = isLast ? LayoutElementMinWidthIsLast : LayoutElementMinWidth;
        }
    }
}
