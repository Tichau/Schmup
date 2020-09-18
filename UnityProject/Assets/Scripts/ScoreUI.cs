using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private Text label;

    private void Start()
    {
        Debug.Assert(this.label != null);

        this.label.text = $"Your score: {GameManager.Instance.Score:000 000 000}\nBest score: {GameManager.Instance.BestScore:000 000 000}";
    }
}
