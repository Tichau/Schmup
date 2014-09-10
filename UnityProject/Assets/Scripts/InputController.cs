// <copyright file="InputController.cs" company="1WeekEndStudio">Copyright 1WeekEndStudio. All rights reserved.</copyright>

using UnityEngine;

public class InputController : MonoBehaviour
{
    private Engines engines;
    private BulletGun[] bulletGuns;

    private PlayerAvatar playerAvatar;

    private void Start()
    {
        // Retrieve the player avatar of the game object.
        this.playerAvatar = this.GetComponent<PlayerAvatar>();

        if (this.playerAvatar == null)
        {
            Debug.LogWarning("There is no player avatar on the game object.");
        }

        // Retrieve the engines of the game object.
        this.engines = this.GetComponent<Engines>();

        if (this.engines == null)
        {
            Debug.LogWarning("There is no engines on the game object.");
        }

        // Retrieve the bullet guns of the game object.
        this.bulletGuns = this.GetComponents<BulletGun>();

        if (this.bulletGuns == null || this.bulletGuns.Length == 0)
        {
            Debug.LogWarning("There is no bullet guns on the game object.");
        }
    }

    private void Update()
    {
        if (this.engines != null)
        {
            float horizontalSpeedPercent = Input.GetAxis("Horizontal");
            float verticalSpeedPercent = Input.GetAxis("Vertical");

            this.engines.SetSpeed(new Vector2(horizontalSpeedPercent, verticalSpeedPercent));
        }

        if (this.bulletGuns != null)
        {
            for (int index = 0; index < this.bulletGuns.Length; index++)
            {
                BulletGun bulletGun = this.bulletGuns[index];

                if (Input.GetAxis("Fire") > 0f)
                {
                    bulletGun.TryToFire();
                }
            }
        }

        if (this.playerAvatar != null)
        {
            if (Input.GetAxis("SwitchWeapon") > 0f)
            {
                this.playerAvatar.SwitchToNextWeapon();
            }
        }
    }
}
