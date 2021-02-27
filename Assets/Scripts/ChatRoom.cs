using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRoom : IDisposable
{
    public List<User> RoomUsers { get; } = new List<User>();
    public List<Message> roomMessages { get; } = new List<Message>();
    public Dictionary<int, Message> lastMessages = new Dictionary<int, Message>();
    public Message LastMessageInRoom { get; private set; }

    public event Action<Message, Message> LastMessageChanged = (oldM, newM) => { };

    public void Init()
    {
        UChatApp.Instance.ChatController.MessageSended += OnMessageSended;
    }

    public void Dispose()
    {
        UChatApp.Instance.ChatController.MessageSended -= OnMessageSended;
    }

    private void OnMessageSended(Message message)
    {
        lastMessages[message.Sender.Id] = message;
        roomMessages.Add(message);

        var oldLastMessage = LastMessageInRoom;
        LastMessageInRoom = message;
        LastMessageChanged(oldLastMessage, LastMessageInRoom);
    }
}
