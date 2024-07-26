using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    [SerializeField] private bool _showLogs = false;
    [SerializeField] private Color _color;

    public void Log(object message, Object sender)
    {
        var color = ColorUtility.ToHtmlStringRGB(_color);
        if (!_showLogs) return;
        Debug.Log("<color=#" + color + ">" + message + "</color>", sender);
    }
}
