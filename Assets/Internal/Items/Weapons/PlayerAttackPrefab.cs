using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackType
{
    Melee,
    Projectile
}

public class PlayerAttackPrefab : MonoBehaviour, IHasTriggerEnter
{
    public bool DestroyWhenOutside;
    public bool DestroyOnContact;
    
    public PlayerAttackType AttackType;

    [Header("Fields for BASIC attacks only")]
    public int Damage;
    public float Knockback;
    public float KnockbackTime = 0;

    private void Awake()
    {
        if (gameObject.TryGetComponent(out Collider2D _))
        {
            gameObject.AddComponent<CallsTriggerCollisions>();
        }
    }

    public virtual void Start()
    {
        transform.localScale *= GlobalPlayer.GetStatValue(PlayerStatEnum.attackSize);
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    public void SetKnockback(float knockback)
    {
        Knockback = knockback;
    }

    public void SetKnockbackTime(float time)
    {
        KnockbackTime = time;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion") && AttackType == PlayerAttackType.Projectile)
        {
            if (Global.itemPassiveManager.GetPassive(ItemPassiveEnum.ProjectileThroughExplosion))
            {
                Damage = Mathf.CeilToInt(Damage * Global.itemPassiveManager.ProjectileThroughExplosionMultiplier);
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public void OnTriggerEnterEvent(GameObject collisionObject)
    {
        

        if (collisionObject.CompareTag("Enemy"))
        {
            if (collisionObject.TryGetComponent(out EnemyGetHit hit))
            {
                int damage = 0;
                switch (AttackType)
                {
                    case PlayerAttackType.Projectile:
                        damage = Mathf.FloorToInt(Damage * GlobalPlayer.CurrentPlayerDamageMultiplier * 
                            GlobalPlayer.GetStatValue(PlayerStatEnum.damage) * GlobalPlayer.GetStatValue(PlayerStatEnum.projectileDamage));

                        break;

                    case PlayerAttackType.Melee:
                        damage = Mathf.FloorToInt(Damage * GlobalPlayer.CurrentPlayerDamageMultiplier *
                            GlobalPlayer.GetStatValue(PlayerStatEnum.damage) * GlobalPlayer.GetStatValue(PlayerStatEnum.meleeDamage));
                        break;

                }
                EventManager.TriggerEvent(EventStrings.ENEMY_HIT, null);
                hit.GetHit(damage, transform.position);
            }

            if (collisionObject.TryGetComponent(out EnemyMovement move))
            {
                move.DoKnockback(Knockback, KnockbackTime, null);
            }

            if (DestroyOnContact)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void Update()
    {
        if (DestroyWhenOutside
            && (transform.position.x > 17.5f
            || transform.position.x < -20f
            || transform.position.y < -20f
            || transform.position.y > 20f
            ))
        {
            Destroy(gameObject);
        }
    }
}
