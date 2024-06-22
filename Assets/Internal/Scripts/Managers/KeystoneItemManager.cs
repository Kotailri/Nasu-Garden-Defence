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
    public int MovespeedLevelIncrease;

    public void ActivateApexStride()
    {
        GlobalItemToggles.HasApexStride = true;
    }

    [Space(10f)]
    [Header("Amplification")]
    [Range(0.01f, 1f)]
    public float DistanceAmplificationAmount = 1;

    public void ActivateAmplification()
    {
        GlobalItemToggles.HasAmplifier = true;
    }

    [Space(10f)]
    [Header("Bwo")]
    public GameObject BwoPrefab;
    public float BwoMovespeed;
    [HideInInspector]
    public bool CanBwoShoot = true;

    public void ActivateBwo()
    {
        GlobalItemToggles.HasBwo = true;
        Instantiate(BwoPrefab, Global.playerTransform.position, Quaternion.identity);
    }

    [Space(10f)]
    [Header("Purple Shed")]
    public GameObject PurpleShedPrefab;
    public float ShedTimer;

    public void ActivatePurpleShed()
    {
        GlobalItemToggles.HasPurpleShed = true;
    }

    [Space(10f)]
    [Header("Immortal Harmony")]
    [Range(1f, 10f)]
    public float EnemyMovespeedAmplifier;

    public void ActivateImmortalHarmony()
    {
        GlobalItemToggles.HasImmortalHarmony = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { ActivateWinterCoat(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ActivateApexStride(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ActivateAmplification(); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { ActivateBwo(); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { ActivatePurpleShed(); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { ActivateImmortalHarmony(); }
    }
}
