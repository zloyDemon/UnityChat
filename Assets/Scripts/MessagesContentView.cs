using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesContentView : MonoBehaviour
{
    [SerializeField] MessageItem messageItemPrefab;
    [SerializeField] RectTransform scrollContent;

    private void Awake()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged += OnMessageSended;
    }

    private void OnDestroy()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged -= OnMessageSended;
    }

    private void OnMessageSended(Message messageO, Message messageN)
    {
        var newMessageItem = Instantiate(messageItemPrefab);
        newMessageItem.transform.SetParent(scrollContent.transform);
        newMessageItem.Init(messageN);

        if (messageO != null)
        {
            bool isSame = messageO.Sender.Id == messageN.Sender.Id;
            newMessageItem.CalculateTopPadding(isSame);
        }
    }
}
