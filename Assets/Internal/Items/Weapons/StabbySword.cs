using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbySword : PlayerAttackPrefab, ISwordAttack
{
    [Header("Sword Stab")]
    public float StartDelay;
    public float StabDuration;
    public float StabDistance;

    [Space(5f)]
    public GameObject sword;

    public override void Start()
    {
        base.Start();

        sword.GetComponent<PlayerAttackPrefab>().SetDamage(Damage);
        sword.GetComponent<PlayerAttackPrefab>().SetKnockback(Knockback);
        sword.GetComponent<PlayerAttackPrefab>().SetKnockbackTime(KnockbackTime);
        sword.GetComponent<PlayerAttackPrefab>().SetKnockbackType(KnockbackType);

        sword.GetComponent<BoxCollider2D>().enabled = false;

        LeanTween.alpha(sword, 1, StartDelay).setOnComplete(()=>
        {
            StartCoroutine(DelayHitboxReEnable());
            LeanTween.alpha(sword, 0, StabDuration);
            LeanTween.moveLocalX(sword, StabDistance, StabDuration).setOnComplete(() => {
                sword.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(sword);
            });
        });

        IEnumerator DelayHitboxReEnable()
        {
            yield return new WaitForSeconds(StabDuration / 3f);
            sword.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }
}
