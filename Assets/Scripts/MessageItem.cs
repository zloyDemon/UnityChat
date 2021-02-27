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
        messageBubble.SetMessageData(message.MessageText, "13.05.2020", isOwner);
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
        var newPadding = new RectOffset(padding.left, padding.right, padding.top - topPaddingDelta, padding.bottom);
        horizontalLayout.padding = newPadding;
    }
}