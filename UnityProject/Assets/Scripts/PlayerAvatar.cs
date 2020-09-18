// <copyright file="PlayerAvatar.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class PlayerAvatar : BaseAvatar
{
    [SerializeField]
    private string[] weaponsNames;

    [SerializeField]
    private float weaponSwitchCooldown;

    [SerializeField]
    private bool invincible;

    private float lastWeaponSelectionChangeTime;
    private int selectedWeapon;

    public string SelectedWeaponName => this.weaponsNames[this.selectedWeapon];

    public bool IsDead => this.HealthPoint <= 0;

    public override void TakeDamage(float damage)
    {
        if (this.invincible)
        {
            return;
        }

        base.TakeDamage(damage);
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

        for (int index = 0; index < this.BulletGuns.Length; index++)
        {
            BulletGun bulletGun = this.BulletGuns[index];
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
