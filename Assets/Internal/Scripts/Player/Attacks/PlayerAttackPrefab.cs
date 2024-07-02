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
    private int Damage;

    private void Awake()
    {
        gameObject.AddComponent<CallsTriggerCollisions>();
    }

    private void Start()
    {
        switch (AttackType)
        {
            case PlayerAttackType.Projectile:
                transform.localScale *= GlobalPlayer.GetStatValue(PlayerStatEnum.projectileSize);
                break;

            case PlayerAttackType.Melee:
                transform.localScale *= GlobalPlayer.GetStatValue(PlayerStatEnum.meleeAttackSize);
                break;

        }
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
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
                hit.GetHit(damage);
            }

            if (DestroyOnContact)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (DestroyWhenOutside && transform.position.x >= Global.MaxX)
        {
            Destroy(gameObject);
        }
    }
}
