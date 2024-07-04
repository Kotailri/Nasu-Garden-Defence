using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuSwordSwingSpawner : PlayerAttack
{
    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        GameObject g = Instantiate(AttackPrefab, transform.position, Quaternion.identity);
        if (attachObject != null)
        {
            g.transform.SetParent(attachObject);
        }
        else
        {
            g.transform.SetParent(Global.playerTransform);
        }

        g.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
        g.transform.localPosition = Vector3.zero;
    }
}
