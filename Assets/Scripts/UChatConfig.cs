using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UChatConfig
{
    private static List<User> users = new List<User>()
    {
        new User(UChatUtils.GenerateId(), "Jack Joyce", "avatar_1"),
        new User(UChatUtils.GenerateId(), "Alan Wake", "avatar_2"),
        new User(UChatUtils.GenerateId(), "Max Payne", "avatar_3"),
    };

    public static User GetRandomOwner()
    {
        int index = Random.Range(0, users.Count - 1);
        return users[index];
    }
}
