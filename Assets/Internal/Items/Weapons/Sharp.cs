using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : PlayerAttackPrefab
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySound(AttackHitSound);
            AttackHitsEnemy(collision.gameObject);
        }
    }
}
