// <copyright file="GUIManager.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private float guiHeight = 30f;

    [SerializeField]
    private Texture backgroundTexture;

    [SerializeField]
    private Texture healthBarTexture;

    [SerializeField]
    private GUISkin guiSkin;

    private void OnGUI()
    {
        // Background.
        GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, Screen.width, guiHeight), this.backgroundTexture);

        // HP Bar.
        PlayerAvatar playerAvatar = GameManager.Instance.PlayerAvatar;
        if (playerAvatar != null)
        {
            float healthRatio = playerAvatar.HealthPoint / playerAvatar.MaximumHealthPoint;
            GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, Screen.width * healthRatio, guiHeight), this.healthBarTexture);
        }
    }
}
