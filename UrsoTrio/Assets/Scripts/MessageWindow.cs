﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectXFormMover))]
public class MessageWindow : MonoBehaviour
{
#region Fields
    public Image messageIcon;
    public Text messageText;
    public Text buttonText;
#endregion

#region Methods
    public void ShowMessage(Sprite sprite = null, string message = "", string buttonMsg = "start")
    {
        if(messageIcon != null)
        {
            messageIcon.sprite = sprite;
        }

        if(messageText != null)
        {
            messageText.text = message;
        }

        if(buttonText != null)
        {
            buttonText.text = buttonMsg;
        }
    }
#endregion
}
