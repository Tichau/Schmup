// <copyright file="PlayerAvatar.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class PlayerAvatar : BaseAvatar
{
    [SerializeField]
    private string[] weaponsNames;

    [SerializeField]
    private float weaponSwitchCooldown;

    private float lastWeaponSelectionChangeTime;
    private int selectedWeapon;

    public string SelectedWeaponName
    {
        get
        {
            return this.weaponsNames[this.selectedWeapon];
        }
    }

    public void SwitchToNextWeapon()
    {
        if (Time.time < this.lastWeaponSelectionChangeTime + this.weaponSwitchCooldown)
        {
            // Can't change the selected weapon now. It's in cooldown.
            return;
        }

        this.lastWeaponSelectionChangeTime = Time.time;
        this.selectedWeapon = (this.selectedWeapon + 1) % this.weaponsNames.Length;

        for (int index = 0; index < this.bulletGuns.Length; index++)
        {
            BulletGun bulletGun = this.bulletGuns[index];
            if (bulletGun.WeaponName == this.weaponsNames[this.selectedWeapon])
            {
                bulletGun.enabled = true;
            }
            else
            {
                bulletGun.enabled = false;
            }
        }
    }
}
