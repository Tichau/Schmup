// <copyright file="InputController.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class InputController : MonoBehaviour
{
    private Engines engines;

    private void Start()
    {
        // Retrieve the engines of the game object.
        this.engines = this.GetComponent<Engines>();

        if (this.engines == null)
        {
            Debug.LogWarning("There is no engines on the game object.");
        }
    }

    private void Update()
    {
        if (this.engines == null)
        {
            return;
        }

        float horizontalSpeedPercent = Input.GetAxis("Horizontal");
        float verticalSpeedPercent = Input.GetAxis("Vertical");
        this.engines.SetSpeed(new Vector2(horizontalSpeedPercent, verticalSpeedPercent));
    }
}
