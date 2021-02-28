using System;
using UnityEngine;

public class ChatController
{
    public event Action<Message> MessageSended = m => { };
    public event Action<Message> MessageRecived = m => { };

    public void SendMessage(string messageText)
    {
        var messageAuthor = UChatConfig.GetRandomOwner();
        Message message = new Message(UChatUtils.GenerateId(), messageText, messageAuthor);
        MessageSended(message);
        MessageRecived(message);
    }
}
