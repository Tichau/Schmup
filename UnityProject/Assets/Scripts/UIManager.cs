// <copyright file="GUIManager.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument gameHUD;
    [SerializeField] private UIDocument gameOverPanel;

    private Label levelName;
    private Label score;

	private ProgressBar energyBar;
	private ProgressBar healthBar;

	private VisualElement activeWeapon;

    private Label gameOverScores;

    private string lastSelectedWeaponName;
	private GameState lastGameState;

    public void OnEnable()
	{
		Debug.Assert(this.gameHUD != null);
		Debug.Assert(this.gameOverPanel != null);

        this.levelName = this.gameHUD.rootVisualElement.Q<Label>("level-name");
		Debug.Assert(this.levelName != null);
		this.levelName.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        this.score = this.gameHUD.rootVisualElement.Q<Label>("score");
		Debug.Assert(this.score != null);
        this.energyBar = this.gameHUD.rootVisualElement.Q<ProgressBar>("energy-bar");
		Debug.Assert(this.energyBar != null);
        this.healthBar = this.gameHUD.rootVisualElement.Q<ProgressBar>("health-bar");
		Debug.Assert(this.healthBar != null);
        this.activeWeapon = this.gameHUD.rootVisualElement.Q<VisualElement>("active-weapon");
		Debug.Assert(this.activeWeapon != null);

        this.gameOverScores = this.gameOverPanel.rootVisualElement.Q<Label>("game-over-scores");
		Debug.Assert(this.gameOverScores != null);
	}

	private void OnDisable()
    {
    }

	private void Update()
    {
		if (GameManager.Instance.State != this.lastGameState)
		{
			this.gameHUD.rootVisualElement.RemoveFromClassList($"game-state--{this.lastGameState}");
			this.gameOverPanel.rootVisualElement.RemoveFromClassList($"game-state--{this.lastGameState}");

			this.gameHUD.rootVisualElement.AddToClassList($"game-state--{GameManager.Instance.State}");
			this.gameOverPanel.rootVisualElement.AddToClassList($"game-state--{GameManager.Instance.State}");

			if (GameManager.Instance.State == GameState.Dead)
			{
				this.gameOverScores.text = $"Your score: {GameManager.Instance.Score:000 000 000}\nBest score: {GameManager.Instance.BestScore:000 000 000}";
			}

			this.lastGameState = GameManager.Instance.State;
		}

        this.score.text = GameManager.Instance.Score.ToString("000 000 000");

		PlayerAvatar playerAvatar = GameManager.Instance.PlayerAvatar;
        if (playerAvatar != null)
        {
			this.healthBar.value = playerAvatar.HealthPoint / playerAvatar.MaximumHealthPoint * 100f;

			this.energyBar.value = playerAvatar.Energy / playerAvatar.MaximumEnergy * 100f;
			if (playerAvatar.IsEnergyRestoring)
			{
				this.energyBar.AddToClassList("restoring");
			}
			else
			{
				this.energyBar.RemoveFromClassList("restoring");
			}

			// Selected weapon icon.
            if (playerAvatar.SelectedWeaponName != this.lastSelectedWeaponName)
            {
				this.activeWeapon.RemoveFromClassList(this.lastSelectedWeaponName);
				this.activeWeapon.AddToClassList(playerAvatar.SelectedWeaponName);
                this.lastSelectedWeaponName = playerAvatar.SelectedWeaponName;
            }
		}
	}
}
