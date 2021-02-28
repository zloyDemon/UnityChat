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
    [SerializeField] RectTransform space;

    public Message CurrentMessage { get; private set; }

    private void Awake()
    {
        deleteMessageButton.gameObject.SetActive(false);
    }

    public void Init(Message message)
    {
        CurrentMessage = message;
        bool isOwner = UChatApp.Instance.IsOwnerUser(message.Sender);
        messageBubble.SetMessageData(message);
        TransformMessageBubble(MessageBubble.MessageBubbleType.Last);
        horizontalLayout.reverseArrangement = !isOwner;
        horizontalLayout.childAlignment = isOwner ? TextAnchor.LowerRight : TextAnchor.LowerLeft;
        var sprite = UChatApp.Instance.Avatars.GetSpriteByName(message.Sender.AvatarId);
        if (sprite != null)
            avatar.sprite = sprite;
        if (isOwner)
            deleteMessageButton.onClick.AddListener(DeleteThisMessage);
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
        space.sizeDelta = Vector2.right * (type == MessageBubble.MessageBubbleType.Last ? -horizontalLayout.spacing : 20);
    }

    public void SetActiveDeleteButton(bool isActive)
    {
        deleteMessageButton.gameObject.SetActive(isActive);
    }

    private void DeleteThisMessage()
    {
        UChatApp.Instance.CurrentChatRoom.DeleteMessageFromRoom(CurrentMessage);
    }
}