using System;
using UnityEngine;

public class ChatController
{
    public event Action<Message> MessageSended = m => { };

    public void SendMessage(string messageText)
    {
        var messageAuthor = UChatApp.Instance.CurrentChatRoom.GetRandomUser();
        Message message = new Message(UChatUtils.GenerateId(), messageText, messageAuthor);
        MessageSended(message);
    }
}
