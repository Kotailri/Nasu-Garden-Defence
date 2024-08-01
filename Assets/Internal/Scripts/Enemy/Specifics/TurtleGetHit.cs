using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGetHit : EnemyGetHit
{
    public bool IsImmune = false;

    public override void GetHit(AttackModuleInfoContainer info)
    {
        if (IsImmune)
        {
            if (info.IsObjectDestroyedByDeflect)
            {
                info.AttackObject.SetActive(false);
            }
            AudioManager.instance.PlaySound(AudioEnum.Dink);
            Managers.Instance.Resolve<IPrefabMng>().InstantiatePrefab(PrefabEnum.DinkEffect, info.HitPosition, Quaternion.identity);
            return;
        }

        base.GetHit(info);
    }
}
