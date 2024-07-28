using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpLauncher : PlayerAttack
{
    [Header("Sharp Launcher")]
    public int NumberOfSharps;
    public int SharpLaunchSpeed;
    public float Radius;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        float angleStep = 360f / NumberOfSharps;
        for (int i = 0; i < NumberOfSharps; i++)
        {
            float angle = i * angleStep;
            float angleRad = angle * Mathf.Deg2Rad;

            GameObject g = Instantiate(AttackPrefab, transform.position, Global.GetRandom2DRotation());

            g.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
            g.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
            g.GetComponent<PlayerAttackPrefab>().SetKnockbackTime(KnockbackTime);
            g.GetComponent<PlayerAttackPrefab>().SetKnockbackType(KnockbackType);

            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            g.GetComponent<Rigidbody2D>().velocity = SharpLaunchSpeed * direction.normalized;
        }
    }
}
