using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    [SerializeField] Image avatar;
    [SerializeField] Button deleteMessageButton;
    [SerializeField] HorizontalLayoutGroup horizontalLayout;
    [SerializeField] MessageBubble messageBubble;
    [SerializeField] RectTransform space;

    public Message CurrentMessage { get; private set; }
    private CanvasGroup canvasGroup;
    private LayoutElement layoutElement;

    private Tween fadeTween;

    private void Awake()
    {
        deleteMessageButton.gameObject.SetActive(false);
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
    }

    public void Init(Message message, bool isSamePrevios = false)
    {
        const int HorizontalLayoutPaddingValue = 80;
        CurrentMessage = message;
        bool isOwner = UChatApp.Instance.IsOwnerUser(message.Sender);
        messageBubble.SetMessageData(message);
        TransformMessageBubble(MessageBubble.MessageBubbleType.Last);
        horizontalLayout.reverseArrangement = !isOwner;
        horizontalLayout.childAlignment = isOwner ? TextAnchor.LowerRight : TextAnchor.LowerLeft;
        if (isOwner)
            horizontalLayout.padding.left = HorizontalLayoutPaddingValue;
        else
            horizontalLayout.padding.right = HorizontalLayoutPaddingValue;
        var sprite = UChatApp.Instance.Avatars.GetSpriteByName(message.Sender.AvatarId);
        if (sprite != null)
            avatar.sprite = sprite;
        if (isOwner)
            deleteMessageButton.onClick.AddListener(DeleteThisMessage);

        if(isSamePrevios)
            CalculateTopPadding();

        AnimateAppearanceItem();

        StartCoroutine(CorWaitFrame());
    }

    private void CalculateTopPadding()
    {
        const int paddingValue = 10;
        var padding = horizontalLayout.padding;
        horizontalLayout.padding.top = padding.top - paddingValue;
    }

    private void AnimateAppearanceItem()
    {
        TweenUtils.KillAndNull(ref fadeTween);
        canvasGroup.alpha = 0;
        float duration = 0.25f;
        fadeTween = canvasGroup.DOFade(1, duration).OnComplete(() => fadeTween = null);
    }

    public void TransformMessageBubble(MessageBubble.MessageBubbleType type)
    {
        messageBubble.TransformMessageBubble(type);
        avatar.color = type == MessageBubble.MessageBubbleType.Last ? Color.white : Color.clear;
    }

    public void SetActiveDeleteButton(bool isActive)
    {
        deleteMessageButton.gameObject.SetActive(isActive);
    }

    private void DeleteThisMessage()
    {
        UChatApp.Instance.CurrentChatRoom.DeleteMessageFromRoom(CurrentMessage);
    }

    private IEnumerator CorWaitFrame()
    {
        yield return new WaitForEndOfFrame();
        Debug.LogWarning(((RectTransform)transform).sizeDelta.y);
    }
}