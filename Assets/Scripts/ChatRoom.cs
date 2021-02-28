﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChatRoom : IDisposable
{
    public List<User> RoomUsers { get; } = new List<User>();
    public List<Message> roomMessages { get; } = new List<Message>();
    public Dictionary<int, Message> lastMessages = new Dictionary<int, Message>();
    public Message LastMessageInRoom { get; private set; }

    public event Action<Message, Message> LastMessageChanged = (oldM, newM) => { };
    public event Action<Message> MessageDeleted = dm => { };

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

    public void DeleteMessageFromRoom(Message message)
    {
        var deleteMessage = roomMessages.FirstOrDefault(m => m.IdMessage == message.IdMessage);
        if (deleteMessage != null)
        {
            roomMessages.Remove(deleteMessage);
            bool hasValue = lastMessages.TryGetValue(deleteMessage.Sender.Id, out Message lastMessage);
            if (hasValue && lastMessage.IdMessage == deleteMessage.IdMessage)
            {
                var newLast = roomMessages.FindLast(m => m.Sender.Id == deleteMessage.Sender.Id);
                if (newLast != null)
                    lastMessages[newLast.Sender.Id] = newLast;
                else
                    lastMessages.Remove(message.Sender.Id);
            }

            MessageDeleted(message);
        }
    }
}
