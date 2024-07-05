using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuSwordSwing : PlayerAttackPrefab
{
    [Header("Sword Swing")]
    public float StartDelay;
    public float SwingSpeed;
    public float DestroyDelay;

    [Space(5f)]
    public bool ReverseSwing;

    public override void Start()
    {
        base.Start();
        GetComponent<BoxCollider2D>().enabled = false;
        if (ReverseSwing)
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        StartCoroutine(SwingTiming());
    }

    private IEnumerator SwingTiming()
    {
        yield return new WaitForSeconds(StartDelay);
        GetComponent<BoxCollider2D>().enabled = true;
        if (ReverseSwing)
            LeanTween.rotateZ(gameObject, 0f, SwingSpeed).setEaseInOutCubic().setOnComplete(() => { Destroy(gameObject, DestroyDelay); });
        else
            LeanTween.rotateZ(gameObject, 180f, SwingSpeed).setEaseInOutCubic().setOnComplete(() => { Destroy(gameObject, DestroyDelay); });
    }
}
