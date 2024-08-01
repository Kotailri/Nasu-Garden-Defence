using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackType
{
    Melee,
    Projectile,
    Explosion,
    Neutral
}

public enum PlayerKnockbackType
{
    AwayFromGarden,
    AwayFromPlayer,
    None
}

public class PlayerAttackPrefab : MonoBehaviour
{
    public bool DestroyWhenOutside;
    public bool DestroyOnContact;
    public bool destroyedByDeflection;


    public PlayerAttackType AttackType;
    public AudioEnum AttackHitSound = AudioEnum.None;

    [Header("Fields for BASIC attacks only")]
    public int Damage;
    public PlayerKnockbackType KnockbackType = PlayerKnockbackType.AwayFromGarden;
    public float Knockback;
    public float KnockbackTime = 0;

    public virtual void Start()
    {
        transform.localScale *= GlobalStats.GetStatValue(PlayerStatEnum.attackSize);
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

    public void SetKnockbackType(PlayerKnockbackType type)
    {
        KnockbackType = type;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            ProjectilePassThroughExplosion();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (AttackType == PlayerAttackType.Projectile && Global.itemPassiveManager.GetPassive(ItemPassiveEnum.ProjectileMightExplode) 
                && Random.Range(0f,1f) < Global.itemPassiveManager.ProjectileExplosionChance)
            {
                GameObject g = Managers.Instance.Resolve<IPrefabMng>().InstantiatePrefab(PrefabEnum.PurpleExplosion, transform.position, Quaternion.identity);
                g.GetComponent<Explosion>().Initialize(Global.itemPassiveManager.ProjectileExplosionDamage);
            }
            AudioManager.instance.PlaySound(AttackHitSound);
            AttackHitsEnemy(collision.gameObject);
        }
    }

    protected void AttackHitsEnemy(GameObject enemy)
    {
        if (enemy.TryGetComponent(out EnemyGetHit hit))
        {
            EventManager.TriggerEvent(EventStrings.ENEMY_HIT, null);

            AttackModuleInfoContainer info = new(Damage, AttackType, gameObject, transform.position)
            {
                IsObjectDestroyedByDeflect = destroyedByDeflection
            };

            hit.GetHit(info);
        }

        if (enemy.TryGetComponent(out EnemyMovement move))
        {
            move.DoKnockback(Knockback, KnockbackType, KnockbackTime, null);
        }

        if (DestroyOnContact)
        {
            Destroy(gameObject);
        }
    }

    private void ProjectilePassThroughExplosion()
    {
        if (AttackType == PlayerAttackType.Projectile)
        {
            if (Global.itemPassiveManager.GetPassive(ItemPassiveEnum.ProjectileThroughExplosion))
            {
                Damage = Mathf.CeilToInt(Damage * Global.itemPassiveManager.ProjectileThruExplosionDmg);
                GetComponent<SpriteRenderer>().color = Color.red;
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
