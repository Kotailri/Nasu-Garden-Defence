using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ApexStrideAdder : ItemAdderNormal
{
    [Header("Apex Stride")]
    public float StrideSpeedMultiplier1;
    public float StrideSpeedMultiplier2;
    public float DamageBoostMultiplier;
    public float TimeBeforeTimeout;

    [HideInInspector]
    public int ApexStrideLevel = 0;

    [Space(5f)]
    public GameObject StrideParticles;
    public GameObject StrideParticlesMAX;

    public void ActivateApexStride()
    {
        GlobalItemToggles.HasApexStride = true;
        Global.playerTransform.gameObject.AddComponent<ApexStride>();
        Global.playerTransform.gameObject.GetComponent<ApexStride>().Initialize(StrideParticles, StrideParticlesMAX, TimeBeforeTimeout, StrideSpeedMultiplier1, StrideSpeedMultiplier2, DamageBoostMultiplier);
    }

    public override void OnItemGet()
    {
        ActivateApexStride();
        base.OnItemGet();
    }
}
