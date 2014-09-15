// <copyright file="FXController.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class FXController : MonoBehaviour
{
    private BaseAvatar avatar;

    private ParticleSystem hitParticleSystem;

    private void Start()
    {
        // Retrieve the player avatar of the game object.
        this.avatar = this.GetComponent<BaseAvatar>();

        if (this.avatar == null)
        {
            Debug.LogWarning("There is no avatar on the game object.");
        }

        // Hit particle system.
        this.hitParticleSystem = this.GetComponentInChildren<ParticleSystem>();

        if (this.hitParticleSystem == null)
        {
            Debug.LogWarning("There is no hit particle system on the game object.");
        }

        this.avatar.OnDamageTaken += this.Avatar_OnDamageTaken;
    }

    private void Avatar_OnDamageTaken(object sender, DamageTakenEventArgs e)
    {
        if (this.hitParticleSystem != null)
        {
            this.hitParticleSystem.Play();
        }
    }
}
