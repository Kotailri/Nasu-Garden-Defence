using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicShoot : PlayerAttack
{
    private void Start()
    {
        SetAttackPrefab(Global.attackPrefabHolder.GetPrefab(AttackPrefabNameEnum.BasicShoot));
        SetRequiredPetFacing(true);
    }

    public override void DoAttack(Vector2 attackPosition, int attackCount)
    {
        if (AttackPrefab != null)
        {
            GameObject projectile = Instantiate(AttackPrefab, attackPosition + new Vector2(0.25f, -0.25f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerScriptableSettings.ProjectileSpeed + PlayerScriptableSettings.PlayerMovespeed, 0);
        }
    }
}
