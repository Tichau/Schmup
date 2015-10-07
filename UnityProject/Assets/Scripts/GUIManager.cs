// <copyright file="GUIManager.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private int guiHeight = 30;

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

    private string lastSelectedWeaponName;
    private Texture selectedWeaponTexture;

    private void OnGUI()
    {
        // Level name
        string currentLevelName = GameManager.Instance.CurrentLevel != null ? GameManager.Instance.CurrentLevel.Name : string.Empty;
        GUI.Label(new Rect(0f, 0f, Screen.width, 60f), currentLevelName, this.guiSkin.label);

        // Background.
        GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, Screen.width, guiHeight), this.backgroundTexture);

        PlayerAvatar playerAvatar = GameManager.Instance.PlayerAvatar;
        if (playerAvatar != null)
        {
            float barHeight = guiHeight / 2f;
            int barWidth = Screen.width - guiHeight;

            // HP Bar.
            float healthRatio = playerAvatar.HealthPoint / playerAvatar.MaximumHealthPoint;
            GUI.DrawTexture(new Rect(0f, Screen.height - guiHeight, barWidth * healthRatio, barHeight), this.healthBarTexture);

            // Energy Bar.
            float energyRatio = playerAvatar.Energy / playerAvatar.MaximumEnergy;
            GUI.DrawTexture(new Rect(0f, Screen.height - barHeight, barWidth * energyRatio, barHeight), playerAvatar.IsEnergyRestoring ? this.energyRestoringBarTexture : this.energyBarTexture);

            // Selected weapon icon.
            if (playerAvatar.SelectedWeaponName != this.lastSelectedWeaponName)
            {
                this.selectedWeaponTexture = (Texture)Resources.Load(string.Format("Textures/{0}Icon", playerAvatar.SelectedWeaponName));
                this.lastSelectedWeaponName = playerAvatar.SelectedWeaponName;
            }

            GUI.DrawTexture(new Rect(barWidth, Screen.height - guiHeight, guiHeight, guiHeight), this.selectedWeaponTexture);
        }
    }
}
