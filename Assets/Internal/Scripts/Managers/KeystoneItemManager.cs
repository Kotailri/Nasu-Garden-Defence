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
    public float BwoMovespeed;

    [Space(10f)]
    [Header("Purple Shed")]
    public GameObject PurpleShedPrefab;
    public float ShedTimer;

    [Space(10f)]
    [Header("Immortal Harmony")]
    [Range(1f, 10f)]
    public float EnemyMovespeedAmplifier;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { ActivateWinterCoat(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ActivateAmplification(); }
    }
}
