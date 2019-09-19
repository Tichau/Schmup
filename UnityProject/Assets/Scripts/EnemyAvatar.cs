// <copyright file="EnemyAvatar.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using UnityEngine;

public class EnemyAvatar : BaseAvatar
{
    [SerializeField]
    private int scoreGainOnDeath;

    public string PrefabPath
    {
        get;
        set;
    }

    protected override void Die()
    {
        base.Die();

        GameManager.Instance.AddScoreGain(this.scoreGainOnDeath);
    }

    protected override void Release()
    {
        GameObject.Destroy(this.gameObject);
    }

    protected override void Update()
    {
        base.Update();

        // Very simple out of bound test.
        if (this.Position.x > 14 || this.Position.x < -14 || this.Position.y > 20 || this.Position.y < -20)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseAvatar avatar = other.gameObject.GetComponent<BaseAvatar>();
        if (avatar != null)
        {
            avatar.TakeDamage(this.DamageDealthAtCollision);
            this.TakeDamage(avatar.DamageDealthAtCollision);
        }
    }
}
