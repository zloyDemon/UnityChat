using UnityEngine;

public class MessagesContentView : MonoBehaviour
{
    [SerializeField] MessageItem messageItemPrefab;
    [SerializeField] RectTransform scrollContent;

    private void Awake()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged += LastMessageUpdated;
    }

    private void OnDestroy()
    {
        UChatApp.Instance.CurrentChatRoom.LastMessageChanged -= LastMessageUpdated;
    }

    private void LastMessageUpdated(Message messageO, Message messageN)
    {
        var newMessageItem = Instantiate(messageItemPrefab, scrollContent.transform, false);
        newMessageItem.Init(messageN);

        if (messageO != null)
        {
            bool isSameSender = messageO.Sender.Id == messageN.Sender.Id;
            newMessageItem.CalculateTopPadding(isSameSender);
        }
    }
}
