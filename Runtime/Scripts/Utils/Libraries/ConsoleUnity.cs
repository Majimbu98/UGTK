// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public static class ConsoleUtility
    {
        public static void LogColored(string message, Color color)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(color);
            string coloredMessage = $"<color=#{hexColor}>{message}</color>";
            Debug.Log(coloredMessage);
        }
    }
}