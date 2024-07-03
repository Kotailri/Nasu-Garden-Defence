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

    public int ApexStrideLevel = 0;
    public float DistanceAmplificationAmount = 0;
    public bool IsBwoFacingAttackDirection = true;
    public float BwoMovespeed = 0;
}
