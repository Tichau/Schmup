using UnityEngine;
using UnityEngine.UI;

public class FadeOutText : MonoBehaviour
{
    [SerializeField]
    private float duration;

    private Text text;

    private float startTime;

    private void Start()
    {
        this.text = this.GetComponent<Text>();
        this.startTime = Time.time;
    }
	
	private void Update()
	{
	    if (this.text.color != Color.clear)
	    {
            this.text.color = Color.Lerp(this.text.color, Color.clear, (Time.time - this.startTime) / this.duration);
	    }
    }
}
