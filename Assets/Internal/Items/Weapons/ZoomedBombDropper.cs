using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomedBombDropper : PlayerAttack
{
    [Header("Zoomed Bomb")]
    public float BombFuseTimer;
    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        AudioManager.instance.PlaySound(AudioEnum.ThingPlaced);
        GameObject g = Instantiate(AttackPrefab, attackPosition, Quaternion.identity);
        g.GetComponent<ZoomedBomb>().Initialize(BombFuseTimer, BaseDamage);
    }
}
