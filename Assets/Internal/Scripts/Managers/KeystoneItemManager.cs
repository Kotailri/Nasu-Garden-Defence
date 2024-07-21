using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeystoneItemManager : MonoBehaviour
{
    private void Awake()
    {
        Global.keystoneItemManager = this;
        ApexStrideLevel = 0;
        DistanceAmplificationAmount = 0;
        BwoMovespeed = 0;
        ImmortalHarmonyShieldTime = 0f;
}

    public int ApexStrideLevel = 0;
    public float DistanceAmplificationAmount = 0;
    public float BwoMovespeed = 0;
    public float ImmortalHarmonyShieldTime = 0f;
}
