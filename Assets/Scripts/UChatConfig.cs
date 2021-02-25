using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UChatConfig
{
    private static List<User> users = new List<User>()
    {
        new User(0, "Jack Joyce", "avatar_1"),
        new User(1, "Alan Wake", "avatar_2"),
        new User(2, "Max Payne", "avatar_3"),
    };

    public static User GetRandomOwner()
    {
        int index = Random.Range(0, users.Count - 1);
        return users[index];
    }
}
