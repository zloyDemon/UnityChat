﻿using UnityEngine;

public class UChatApp : MonoBehaviour
{
    // TODO
    [SerializeField] SpriteHolder avatars;

    public SpriteHolder Avatars => avatars;

    public static UChatApp Instance { get; private set; }

    public User OwnerUser { get; private set; }
    public ChatController ChatController { get; private set; }
    public Room CurrentChatRoom { get; private set; }

    private void Awake()
    {
        if (Instance == this)
            Destroy(gameObject);
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitApp();
    }

    private void InitApp()
    {
        OwnerUser = UChatConfig.GetRandomOwner();
        ChatController = new ChatController();
        CurrentChatRoom = new Room();
        CurrentChatRoom.Init();
    }

    public bool IsOwnerUser(User user)
    {
        return user.Id == OwnerUser.Id;
    }
}
