using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ConfigUserItem : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI nameTmp;
    [SerializeField] private Toggle isOwner;
    [SerializeField] private Button deleteUser;

    public event Action<bool, ConfigUserItem> OnToggleClick = (b,c) => { };
    public event Action<ConfigUserItem> OnDeleteUserClick = c => { };

    public User CurrentUser { get; private set; }

    private void Awake()
    {
        isOwner.onValueChanged.AddListener(OnToggleValueChange);
        deleteUser.onClick.AddListener(OnDeleteClick);
    }

    public void Init(User user)
    {
        avatar.sprite = UChatApp.Instance.Avatars.GetSpriteByName(user.AvatarId);
        nameTmp.text = user.Name;
        CurrentUser = user;
    }

    public void SetToggle(bool flag)
    {
        isOwner.isOn = flag;
    }

    public void SetToggleGroup(ToggleGroup group)
    {
        isOwner.group = group;
    }

    private void OnDeleteClick() => OnDeleteUserClick(this);

    private void OnToggleValueChange(bool flag)
    {
        OnToggleClick(flag, this);
    }
}
