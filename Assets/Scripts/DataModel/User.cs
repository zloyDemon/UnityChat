using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string AvatarId { get; private set; }

    public User (int id, string name, string avatarId)
    {
        Id = id;
        Name = name;
        AvatarId = avatarId;
    }
}
