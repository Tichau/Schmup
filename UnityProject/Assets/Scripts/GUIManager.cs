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
    private Texture energyBarTexture;

    [SerializeField]
    private Texture energyRestoringBarTexture;

    [SerializeField]
    private GUISkin guiSkin;

    private void OnGUI()
    {
        // Background.
        GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, Screen.width, guiHeight), this.backgroundTexture);

        PlayerAvatar playerAvatar = GameManager.Instance.PlayerAvatar;
        if (playerAvatar != null)
        {
            float barHeight = guiHeight / 2f;

            // HP Bar.
            float healthRatio = playerAvatar.HealthPoint / playerAvatar.MaximumHealthPoint;
            GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, Screen.width * healthRatio, barHeight), this.healthBarTexture);

            // Energy Bar.
            float energyRatio = playerAvatar.Energy / playerAvatar.MaximumEnergy;
            GUI.DrawTexture(new Rect(0f, Screen.height - barHeight, Screen.width * energyRatio, barHeight), playerAvatar.IsEnergyRestoring ? this.energyRestoringBarTexture : this.energyBarTexture);
        }
    }
}
