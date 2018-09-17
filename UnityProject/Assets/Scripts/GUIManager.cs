// <copyright file="GUIManager.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private Text levelName;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Image energyBar;

    [SerializeField]
    private Image weaponType;

    [SerializeField]
    private Color energyBarRestoringColor;

    [SerializeField]
    private Sprite simpleGunSprite;

    [SerializeField]
    private Sprite doubleGunSprite;

    [SerializeField]
    private Sprite spiralGunSprite;
    
    private RectTransform healthAndEnergyBarCanvas;

    private string lastSelectedWeaponName;

    private Color energyBarColor;

    private void OnEnable()
    {
        GameManager.Instance.LevelChanged += this.OnGameLevelChange;

        this.healthAndEnergyBarCanvas = this.healthBar.transform.parent.GetComponent<RectTransform>();
        this.energyBarColor = this.energyBar.color;
    }

    private void OnDisable()
    {
        GameManager.Instance.LevelChanged -= this.OnGameLevelChange;
    }

    private void Update()
    {
        PlayerAvatar playerAvatar = GameManager.Instance.PlayerAvatar;
        if (playerAvatar != null)
        {
            // HP Bar.
            float healthRatio = playerAvatar.HealthPoint / playerAvatar.MaximumHealthPoint;
            this.UpdateBarSize(this.healthBar, healthRatio);

            // Energy Bar.
            float energyRatio = playerAvatar.Energy / playerAvatar.MaximumEnergy;
            this.energyBar.color = playerAvatar.IsEnergyRestoring ? this.energyBarRestoringColor : this.energyBarColor;
            this.UpdateBarSize(this.energyBar, energyRatio);

            // Selected weapon icon.
            if (playerAvatar.SelectedWeaponName != this.lastSelectedWeaponName)
            {
                switch (playerAvatar.SelectedWeaponName)
                {
                    case "SimpleGun":
                        this.weaponType.sprite = this.simpleGunSprite;
                        break;

                    case "DoubleGun":
                        this.weaponType.sprite = this.doubleGunSprite;
                        break;

                    case "SpiralGun":
                        this.weaponType.sprite = this.spiralGunSprite;
                        break;

                    default:
                        Debug.LogError("Unknown gun type: " + playerAvatar.SelectedWeaponName);
                        break;
                }

                this.lastSelectedWeaponName = playerAvatar.SelectedWeaponName;
            }
        }
    }

    private void UpdateBarSize(Image bar, float healthRatio)
    {
        Vector2 sizeDelta = bar.rectTransform.sizeDelta;
        sizeDelta.x = (healthRatio - 1) * this.healthAndEnergyBarCanvas.rect.width;
        bar.rectTransform.sizeDelta = sizeDelta;

        Vector3 position = bar.rectTransform.localPosition;
        position.x = sizeDelta.x / 2f;
        bar.rectTransform.localPosition = position;
    }

    private void OnGameLevelChange(object sender, LevelChangedEventArgs eventArgs)
    {
        this.levelName.text = eventArgs.Level.Name;
    }
}
