using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionDuration = 0.5f;

    protected Vector3 scale;

    private void Awake()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    protected virtual void Start()
    {
        DoExplosionEffect();
    }

    protected virtual void DoExplosionEffect()
    {
        LeanTween.scale(gameObject, scale * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionRadius), explosionDuration).setEaseOutExpo().setOnComplete(() => { Destroy(gameObject); });
    }
}
