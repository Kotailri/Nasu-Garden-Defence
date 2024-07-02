using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeystoneItemManager : MonoBehaviour
{
    private void Awake()
    {
        Global.keystoneItemManager = this;
    }

    [Header("Winter Coat")]
    public ItemScriptable WinterCoatItemInfo;
    public GameObject WinterCoatPrefab;
    public float SlowAmount;
    public float SlowTime;

    public void ActivateWinterCoat()
    {
        GameObject winterCoat = Instantiate(WinterCoatPrefab, Vector3.zero, Quaternion.identity);
        winterCoat.transform.SetParent(Global.playerTransform, false);
        GlobalItemToggles.HasWinterCoat = true;
        winterCoat.GetComponent<WinterCoat>().ActivateSlow(SlowAmount, SlowTime);
    }

    [Space(10f)]

    [Header("Apex Stride")]
    public ItemScriptable ApexStrideItemInfo;
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

    [Space(10f)]

    [Header("Amplification")]
    public ItemScriptable AmplificationItemInfo;
    [Range(0.01f, 1f)]
    public float DistanceAmplificationAmount = 1;

    public void ActivateAmplification()
    {
        GlobalItemToggles.HasAmplifier = true;
    }

    [Space(10f)]

    [Header("Bwo")]
    public ItemScriptable BwoItemInfo;
    public GameObject BwoPrefab;
    public float BwoMovespeed;
    [HideInInspector]
    public bool IsBwoFacingAttackDirection = true;

    public void ActivateBwo()
    {
        GlobalItemToggles.HasBwo = true;
        Instantiate(BwoPrefab, Global.playerTransform.position, Quaternion.identity);
    }

    [Space(10f)]

    [Header("Purple Shed")]
    public ItemScriptable PurpleShedItemInfo;
    public GameObject PurpleShedPrefab;
    public float ShedTimer;
    public float FurDuration;
    public float ShedSlowAmount;
    public float ShedDamageTimer;
    public int ShedDamage;

    public void ActivatePurpleShed()
    {
        GlobalItemToggles.HasPurpleShed = true;
        Global.playerTransform.gameObject.AddComponent<PurpleFurSpawner>();
        Global.playerTransform.gameObject.GetComponent<PurpleFurSpawner>().Initialize(PurpleShedPrefab, ShedTimer, FurDuration, ShedSlowAmount, ShedDamageTimer, ShedDamage);
    }

    [Space(10f)]

    [Header("Immortal Harmony")]
    public ItemScriptable ImmortalHarmonyItemInfo;
    public float IFrameExtensionDuration;
    public float ExplosionDamage;
    public int HealPerHit;

    public void ActivateImmortalHarmony()
    {
        GlobalItemToggles.HasImmortalHarmony = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            ActivateWinterCoat();
            Global.itemUI.AddItemToUI(WinterCoatItemInfo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { 
            ActivateApexStride();
            Global.itemUI.AddItemToUI(ApexStrideItemInfo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { 
            ActivateAmplification();
            Global.itemUI.AddItemToUI(AmplificationItemInfo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { 
            ActivateBwo();
            Global.itemUI.AddItemToUI(BwoItemInfo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) 
        { 
            ActivatePurpleShed();
            Global.itemUI.AddItemToUI(PurpleShedItemInfo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) 
        { 
            ActivateImmortalHarmony();
            Global.itemUI.AddItemToUI(ImmortalHarmonyItemInfo);
        }
    }
}
