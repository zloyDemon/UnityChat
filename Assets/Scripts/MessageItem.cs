using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    [SerializeField] Image avatar;
    [SerializeField] Button deleteMessageButton;
    [SerializeField] HorizontalLayoutGroup horizontalLayout;
    [SerializeField] MessageBubble messageBubble;

    private void Awake()
    {
        deleteMessageButton.gameObject.SetActive(false);
    }

    public void Init(Message message)
    {
        bool isOwner = UChatApp.Instance.IsOwnerUser(message.Sender);
        messageBubble.SetMessageData(message);
        TransformMessageBubble(MessageBubble.MessageBubbleType.Last);
        horizontalLayout.reverseArrangement = !isOwner;
        horizontalLayout.childAlignment = isOwner ? TextAnchor.LowerRight : TextAnchor.LowerLeft;
        var sprite = UChatApp.Instance.Avatars.GetSpriteByName(message.Sender.AvatarId);
        if (sprite != null)
            avatar.sprite = sprite;
    }

    // TODO
    public void CalculateTopPadding(bool isPreviosMessageSameSender)
    {
        const int paddingValue = 10;
        int topPaddingDelta = isPreviosMessageSameSender ? paddingValue : 0;
        var padding = horizontalLayout.padding;
        horizontalLayout.padding.top = padding.top - topPaddingDelta;
    }

    public void TransformMessageBubble(MessageBubble.MessageBubbleType type)
    {
        messageBubble.TransformMessageBubble(type);
        avatar.color = type == MessageBubble.MessageBubbleType.Last ? Color.white : Color.clear;
        horizontalLayout.spacing = type == MessageBubble.MessageBubbleType.Last ? 10 : 42;
    }
}