using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBoomerangLauncher : PlayerAttack
{
    [Header("Purple Boomerang")]
    public float speed;
    public float distance;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        GameObject g = Instantiate(AttackPrefab, attackPosition, Quaternion.identity);
        g.GetComponent<PurpleBoomerang>().SetDamage(Mathf.RoundToInt(BaseDamage * GlobalPlayer.GetStatValue(PlayerStatEnum.damage) 
            * GlobalPlayer.GetStatValue(PlayerStatEnum.projectileDamage)));
        g.GetComponent<PurpleBoomerang>().SetKnockback(KnockbackAmount);
        g.GetComponent<PurpleBoomerang>().Launch(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed * GlobalPlayer.GetStatValue(PlayerStatEnum.projectileSpeed), distance);
        
    }
}
