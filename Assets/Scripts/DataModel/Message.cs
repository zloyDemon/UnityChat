using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public int IdMessage { get; private set; }
    public string MessageText { get; private set; }
    public User Sender { get; private set; }

    public Message(int id, string message, User sender)
    {
        IdMessage = id;
        MessageText = message;
        Sender = sender;
    }
}
