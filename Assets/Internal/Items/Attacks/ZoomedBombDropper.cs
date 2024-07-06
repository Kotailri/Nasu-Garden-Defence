using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomedBombDropper : PlayerAttack
{
    [Header("Zoomed Bomb")]
    public float BombFuseTimer;
    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        GameObject g = Instantiate(AttackPrefab, attackPosition, Quaternion.identity);
        g.GetComponent<ZoomedBomb>().Initialize(BombFuseTimer, Mathf.RoundToInt(BaseDamage * GlobalPlayer.CurrentPlayerDamageMultiplier * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionDamage)));
    }
}
