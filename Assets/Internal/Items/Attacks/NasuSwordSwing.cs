using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuSwordSwing : PlayerAttackPrefab
{
    [Header("Sword Swing")]
    public float StartDelay;
    public float SwingSpeed;
    public float DestroyDelay;

    public override void Start()
    {
        base.Start();
        StartCoroutine(SwingTiming());
    }

    private IEnumerator SwingTiming()
    {
        yield return new WaitForSeconds(StartDelay);
        LeanTween.rotateZ(gameObject, 180f, SwingSpeed).setEaseInOutCubic().setOnComplete(() => { Destroy(gameObject, DestroyDelay); });
    }
}
