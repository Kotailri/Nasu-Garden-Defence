using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyExplosion : ExplosionEffect
{
    protected override void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        base.Start();
    }

    protected override void DoExplosionEffect()
    {
        GetComponent<Collider2D>().enabled = true;
        base.DoExplosionEffect();
    }
}
