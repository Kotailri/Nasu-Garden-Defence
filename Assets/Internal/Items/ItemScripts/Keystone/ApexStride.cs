using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApexStride : MonoBehaviour
{
    public float TimeBeforeTimeout;

    [Space(5f)]
    public GameObject StrideParticles;
    public GameObject StrideParticlesMAX;

    private Rigidbody2D RB;
    private PlayerMovement playerMovement;
    private float currentTimeoutTime = 0f;

    private int rampingLevel = 0;
    private float currentRampingTime = 0f;

    private float rampingLevel1Time = 2f;
    private float rampingLevel2Time = 5f;

    private float currentSpeedMultiplier = 0f;
    private float level1SpeedMultiplier = 0.5f;
    private float level2SpeedMultiplier = 1.5f;

    private float damageMultiplier = 0f;
    private float currentDamageMultiplier = 0f;

    private GameObject currentParticles;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Initialize(GameObject _strideParticles, GameObject _strideParticlesMax, float _timeBeforeTimeout, float _lvl1speed, float _lvl2speed, float damageMult)
    {
        TimeBeforeTimeout = _timeBeforeTimeout;

        StrideParticles = _strideParticles;
        StrideParticlesMAX = _strideParticlesMax;

        level1SpeedMultiplier = _lvl1speed;
        level2SpeedMultiplier = _lvl2speed;
        damageMultiplier = damageMult;
    }

    private int boostID = -1;
    public void SetRampingLevel(int level)
    {
        if (level == rampingLevel)
        {
            return;
        }

        Global.keystoneItemManager.ApexStrideLevel = level;
        GlobalPlayer.CurrentPlayerDamageMultiplier -= currentDamageMultiplier;
        GlobalPlayer.GetStat(PlayerStatEnum.movespeed).RemoveStatMultiplier(boostID);

        Destroy(currentParticles);
        switch (level)
        {
            case 0:
                currentRampingTime = 0f;
                currentSpeedMultiplier = 0;
                break;

            case 1:
                currentParticles = Instantiate(StrideParticles, Vector2.zero, Quaternion.identity);
                currentParticles.transform.SetParent(transform, false);
                currentSpeedMultiplier = level1SpeedMultiplier;
                break;

            case 2:
                currentParticles = Instantiate(StrideParticlesMAX, Vector2.zero, Quaternion.identity);
                currentParticles.transform.SetParent(transform, false);
                currentSpeedMultiplier = level2SpeedMultiplier;
                currentDamageMultiplier = damageMultiplier;
                break;
        }
        GlobalPlayer.CurrentPlayerDamageMultiplier += currentDamageMultiplier;
        boostID = GlobalPlayer.GetStat(PlayerStatEnum.movespeed).AddStatMultiplier(currentSpeedMultiplier);
        rampingLevel = level;
    }

    private void Update()
    {
        if (RB.velocity != Vector2.zero)
        {
            currentTimeoutTime = 0f;
        }
        else
        {
            currentTimeoutTime += Time.deltaTime;
            if (currentTimeoutTime >= TimeBeforeTimeout)
            {
                SetRampingLevel(0);
                
            }
        }
        
        if (currentRampingTime >= rampingLevel2Time)
        {
            SetRampingLevel(2);
        }
        else 
        if (currentRampingTime >= rampingLevel1Time)
        {
            SetRampingLevel(1);
            currentRampingTime += Time.deltaTime;
        }
        else
        {
            currentRampingTime += Time.deltaTime;
        }
    }
}

/*
if (Global.keystoneItemManager.ApexStrideLevel == 2)
{
    RB.velocity *= Global.keystoneItemManager.StrideSpeedMultiplier2;
}
else
        if (Global.keystoneItemManager.ApexStrideLevel == 1)
{
    RB.velocity *= Global.keystoneItemManager.StrideSpeedMultiplier1;
}
*/