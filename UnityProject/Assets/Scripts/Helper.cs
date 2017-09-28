// <copyright file="Helper.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public static class Helper
{
    public static void DrawRect(Rect rect, Color color)
    {
        UnityEngine.Debug.DrawLine(rect.position, rect.position + new Vector2(rect.width, 0f), color);
        UnityEngine.Debug.DrawLine(rect.position + new Vector2(rect.width, 0f), rect.position + new Vector2(rect.width, rect.height), color);
        UnityEngine.Debug.DrawLine(rect.position + new Vector2(rect.width, rect.height), rect.position + new Vector2(0, rect.height), color);
        UnityEngine.Debug.DrawLine(rect.position + new Vector2(0f, rect.height), rect.position, color);
    }
}
