using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConfigView : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button addUser;
    [SerializeField] private ConfigUserItem configUserItemPrefab;
    [SerializeField] private RectTransform scrollContent;
    [SerializeField] private RectTransform avatarsContainer;
    [SerializeField] private Button enterChat;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private TextMeshProUGUI warningText;

    private List<ConfigUserItem> configUserItems = new List<ConfigUserItem>();
    private ConfigUserItem choosenUser;
    private List<User> users = new List<User>();
    private Coroutine corShowWarning;

    private void Awake()
    {
        addUser.onClick.AddListener(OnAddUserClick);
        configUserItemPrefab.gameObject.SetActive(false);
        enterChat.onClick.AddListener(EnterChatButtonClick);
        warningText.text = string.Empty;
        InitAvatarsContainer();
    }

    private void EnterChatButtonClick()
    {
        if (users.Count < 2)
        {
            ShowWarning("Users must be more than 2.");
            return;
        }

        if (choosenUser == null)
        {
            ShowWarning("You must choose the owner.");
            return;
        }

        UChatApp.Instance.SetOwner(choosenUser.CurrentUser);
        UChatApp.Instance.CurrentChatRoom.SetRoomUsers(users);

        gameObject.SetActive(false);
    }

    private void OnAddUserClick()
    {
        string name = inputField.text;

        if(string.IsNullOrEmpty(name))
            return;

        string avatarId = String.Empty;
        var sprite = avatar.GetComponent<Image>().sprite;

        if (sprite == null)
            avatarId = UChatApp.Instance.Avatars.GetRandomSprite().name;
        else
            avatarId = sprite.name;

        var newItem = Instantiate(configUserItemPrefab, scrollContent, false);
        var user = new User(UChatUtils.GenerateId(), name, avatarId);
        users.Add(user);
        newItem.Init(user);
        newItem.OnToggleClick += OnItemToggleChangedValue;
        newItem.OnDeleteUserClick += OnUserItemDeleteClick;
        newItem.gameObject.SetActive(true);
        newItem.SetToggleGroup(toggleGroup);
        configUserItems.Add(newItem);

        ClearInput();
    }

    private void InitAvatarsContainer()
    {
        var avatars = UChatApp.Instance.Avatars;
        foreach (var avatarsSprite in avatars.Sprites)
        {
            var go = new GameObject();
            var image = go.AddComponent<Image>();
            image.GetComponent<RectTransform>().sizeDelta = Vector2.one * 50;
            var button = go.AddComponent<Button>();
            button.targetGraphic = image;
            image.sprite = avatarsSprite;
            go.transform.SetParent(avatarsContainer);
            button.onClick.AddListener(() => OnAvatarItemInContainerClick(image));
        }
    }

    private void OnAvatarItemInContainerClick(Image image)
    {
        avatar.sprite = image.sprite;
    }

    private void OnItemToggleChangedValue(bool flag, ConfigUserItem item)
    {
        if (!flag)
            return;

        SetOwner(item);
    }

    private void SetOwner(ConfigUserItem item)
    {
        if (!ReferenceEquals(choosenUser, item))
            choosenUser = item;
    }

    private void ClearInput()
    {
        avatar.sprite = null;
        inputField.text = string.Empty;
    }

    private void OnUserItemDeleteClick(ConfigUserItem item)
    {
        users.Remove(item.CurrentUser);
        configUserItems.Remove(item);
        if (choosenUser == item)
        {
            if(configUserItems.Count > 0)
                SetOwner(configUserItems.First());
        }

        item.OnDeleteUserClick -= OnUserItemDeleteClick;
        item.OnToggleClick -= OnItemToggleChangedValue;

        Destroy(item.gameObject);
    }

    private void ShowWarning(string message)
    {
        if (corShowWarning != null)
        {
            StopCoroutine(corShowWarning);
            corShowWarning = null;
        }

        corShowWarning = StartCoroutine(CorShowWarning(message));
    }

    private IEnumerator CorShowWarning(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(2);
        warningText.text = string.Empty;
        corShowWarning = null;
    }
}
