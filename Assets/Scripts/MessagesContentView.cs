using System.Collections.Generic;
using UnityEngine;

public class MessagesContentView : MonoBehaviour
{
    [SerializeField] MessageItem messageItemPrefab;
    [SerializeField] RectTransform scrollContent;

    private MessageItem lastMessageItem;

    private List<MessageItem> messageItems = new List<MessageItem>();

    private Dictionary<int, MessageItem> usersLastMessages = new Dictionary<int, MessageItem>();

    private void Awake()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged += LastMessageUpdated;
    }

    private void OnDestroy()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged -= LastMessageUpdated;
    }

    private void LastMessageUpdated(Message messageO, Message messageN)
    {
        var newMessageItem = Instantiate(messageItemPrefab, scrollContent.transform, false);
        newMessageItem.Init(messageN);

        if (messageO != null)
        {
            bool isSameSender = messageO.Sender.Id == messageN.Sender.Id;
            newMessageItem.CalculateTopPadding(isSameSender);
            if (isSameSender)
            {
                if (usersLastMessages.ContainsKey(messageN.Sender.Id))
                {
                    var oldSenderMessage = usersLastMessages[messageN.Sender.Id];
                    oldSenderMessage.TransformMessageBubble(MessageBubble.MessageBubbleType.Usual);
                }
            }
        }

        usersLastMessages[messageN.Sender.Id] = newMessageItem;
        messageItems.Add(newMessageItem);
    }
}
