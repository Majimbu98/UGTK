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

        public static void NullReference(string message)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(Color.blue);
            string coloredMessage = $"<color=#{hexColor}>{message}</color>";
            Debug.Log(coloredMessage);
        }
        
        public static void ErrorColored(string message)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(Color.red);
            string coloredMessage = $"<color=#{hexColor}>{message}</color>";
            Debug.Log(coloredMessage);
        }
    }
}