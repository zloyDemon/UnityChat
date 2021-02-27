using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public int IdMessage { get; }
    public string MessageText { get; }
    public User Sender { get; }
    public DateTime MessageTime { get; }

    public Message(int id, string message, User sender)
    {
        IdMessage = id;
        MessageText = message;
        Sender = sender;
        MessageTime = DateTime.Now;
    }
}
