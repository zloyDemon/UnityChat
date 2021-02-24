using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesContentView : MonoBehaviour
{
    [SerializeField] MessageView messageViewPrefab;
    [SerializeField] RectTransform scrollContent;
    [SerializeField] InputMessageArea inputArea;

    private void Awake()
    {
        inputArea.OnSendImageClick += OnMessageSended;
    }

    private void OnDestroy()
    {
        inputArea.OnSendImageClick -= OnMessageSended;
    }

    private void OnMessageSended(string text)
    {
        var mv = Instantiate(messageViewPrefab);
        mv.SetText(text);
        mv.transform.SetParent(scrollContent.transform);
    }
}
