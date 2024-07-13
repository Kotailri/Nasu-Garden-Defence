using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : PlayerAttackPrefab
{
    public float TimeAlive;
    public override void Start()
    {
        base.Start();
        Destroy(gameObject, TimeAlive);
    }
}
