using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputMessageArea : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button sendMessageButton;

    public event Action<string> OnSendImageClick = t => { };

    private void Awake()
    {
        sendMessageButton.onClick.AddListener(OnSendMessageClick);
    }

    private void OnSendMessageClick()
    {
        var text = inputField.text;
        inputField.text = string.Empty;
        OnSendImageClick(text);
    }
}
