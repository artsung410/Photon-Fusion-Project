using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("ChattingBox_Set")]
    [SerializeField] GameObject chattingBoxSet;

    [Header("Scroll_View")]
    [SerializeField] TextMeshProUGUI chatText;
    [SerializeField] Scrollbar chatScrollbar;

    [Header("Input_Field")]
    public TMP_InputField chatBox;
    public static PlayerHUD Instance;

    private void Awake()
    {
        Instance = this;
        Player.ChatSignal += setUpChatMessage;
    }

    private void setUpChatMessage(string message)
    {
        chatText.text += message;
        chatScrollbar.value = 0f;
        chatBox.text = "";
    }

    // ChatBox
    public void DeActivationChattingBoxUI()
    {
        chattingBoxSet.SetActive(false);
    }

    public void ActivationChattingBoxUI()
    {
        chattingBoxSet.SetActive(true);
    }

    public bool IsActivationChattingBoxUI()
    {
        return chattingBoxSet.activeSelf;
    }

}
