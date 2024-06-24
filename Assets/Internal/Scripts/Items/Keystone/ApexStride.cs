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
    private float currentTimeoutTime = 0f;

    private int rampingLevel = 0;
    private float currentRampingTime = 0f;

    private float rampingLevel1Time = 2f;
    private float rampingLevel2Time = 5f;

    private GameObject currentParticles;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    public void Initialize(GameObject _strideParticles, GameObject _strideParticlesMax, float _timeBeforeTimeout)
    {
        TimeBeforeTimeout = _timeBeforeTimeout;

        StrideParticles = _strideParticles;
        StrideParticlesMAX = _strideParticlesMax;
    }

    public void SetRampingLevel(int level)
    {
        if (level == rampingLevel)
        {
            return;
        }

        Global.keystoneItemManager.ApexStrideLevel = level;

        Destroy(currentParticles);
        switch (level)
        {
            case 0:
                currentRampingTime = 0f;
                break;

            case 1:
                currentParticles = Instantiate(StrideParticles, Vector2.zero, Quaternion.identity);
                currentParticles.transform.SetParent(transform, false);
                break;

            case 2:
                currentParticles = Instantiate(StrideParticlesMAX, Vector2.zero, Quaternion.identity);
                currentParticles.transform.SetParent(transform, false);
                break;
        }

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
