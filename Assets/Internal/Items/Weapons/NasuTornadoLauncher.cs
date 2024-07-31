using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuTornadoLauncher : PlayerAttack
{
    [Header("TornadoLauncher")]
    public float speed;
    public float distance;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        GameObject g = Instantiate(AttackPrefab, attackPosition, Quaternion.identity);
        g.GetComponent<NasuTornado>().SetKnockback(KnockbackAmount);
        g.GetComponent<NasuTornado>().SetDamage(BaseDamage);
        g.GetComponent<NasuTornado>().Launch(new Vector2(1, Random.Range(-0.8f, 0.8f)).normalized * speed * GlobalStats.GetStatValue(PlayerStatEnum.projectileSpeed), distance);

    }
}
