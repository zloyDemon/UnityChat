using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagesContentView : MonoBehaviour
{
    [SerializeField] MessageItem messageItemPrefab;
    [SerializeField] RectTransform scrollContent;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button sendMessageButton;
    [SerializeField] Button deleteMessageButton;
    [SerializeField] RectTransform deleteMessageArea;
    [SerializeField] Button readyButton;

    private List<MessageItem> messageItems = new List<MessageItem>();
    private Dictionary<int, MessageItem> usersLastMessages = new Dictionary<int, MessageItem>();

    private void Awake()
    {
        sendMessageButton.onClick.AddListener(OnSendMessageClick);
        deleteMessageButton.onClick.AddListener(OnDeleteMessagesButtonClick);
        readyButton.onClick.AddListener(OnReadyButtonClick);
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged += LastMessageUpdated;
        UChatApp.Instance.CurrentChatRoom.MessageDeleted += OnMessageDeleted;
        deleteMessageArea.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged -= LastMessageUpdated;
        UChatApp.Instance.CurrentChatRoom.MessageDeleted -= OnMessageDeleted;
    }

    private void LastMessageUpdated(Message messageO, Message messageN)
    {
        /*
           This is a bad solution, i know. Instead of Instantiate and Destroy, better to use pool objects.
           For ScrollRect it’s necessary to create special view like RecyclerView in Android. It, however,
           will take more time, so I’ll leave it that way for now.
         */
        var newMessageItem = Instantiate(messageItemPrefab, scrollContent.transform, false);
        bool isSameSender = messageO != null && messageO.Sender.Id == messageN.Sender.Id;
        newMessageItem.Init(messageN, isSameSender);
        if (isSameSender)
        {
            if (usersLastMessages.ContainsKey(messageN.Sender.Id))
            {
                var oldSenderMessage = usersLastMessages[messageN.Sender.Id];
                oldSenderMessage.TransformMessageBubble(MessageBubble.MessageBubbleType.Usual);
            }
        }

        usersLastMessages[messageN.Sender.Id] = newMessageItem;
        messageItems.Add(newMessageItem);
    }

    private void OnSendMessageClick()
    {
        string messageText = inputField.text;
        inputField.text = string.Empty;
        UChatApp.Instance.CurrentChatRoom.SendMessage(messageText);
    }

    private void OnDeleteMessagesButtonClick()
    {
        deleteMessageArea.gameObject.SetActive(true);
        SetActiveOwnerDeleteButtonsOnMessages(true);
    }

    private void OnReadyButtonClick()
    {
        deleteMessageArea.gameObject.SetActive(false);
        SetActiveOwnerDeleteButtonsOnMessages(false);
    }

    private void SetActiveOwnerDeleteButtonsOnMessages(bool isActive)
    {
        var idOwner = UChatApp.Instance.OwnerUser.Id;
        foreach (var messageItem in messageItems)
        {
            if (messageItem.CurrentMessage.Sender.Id == idOwner)
                messageItem.SetActiveDeleteButton(isActive);
        }
    }

    private void OnMessageDeleted(Message message)
    {
        var messageItem = messageItems.FirstOrDefault(m => m.CurrentMessage.IdMessage == message.IdMessage);
        if (messageItem != null)
        {
            messageItems.Remove(messageItem);
            bool isLastMessage = usersLastMessages.TryGetValue(messageItem.CurrentMessage.Sender.Id, out MessageItem lastMessageItem);
            if (isLastMessage)
            {
                var id = message.Sender.Id;
                var newLast = messageItems.LastOrDefault(m => m.CurrentMessage.Sender.Id == id);
                if (newLast != null)
                {
                    usersLastMessages[id] = newLast;
                    newLast.TransformMessageBubble(MessageBubble.MessageBubbleType.Last);
                }
                else
                {
                    usersLastMessages.Remove(id);
                }
            }

            Destroy(messageItem.gameObject);
        }
    }
}