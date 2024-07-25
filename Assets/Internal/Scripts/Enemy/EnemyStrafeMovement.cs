using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafeMovement : EnemyMovement
{
    [Tooltip("Height of the wave")]
    public float waveAmplitude;
    [Tooltip("Time it takes per oscillation")]
    public float waveFrequency;
    public bool startsUp;
    public bool startsRandom;

    [Space(10f)]
    public LeanTweenType easeType = LeanTweenType.easeInOutSine;

    public override void StartMovement()
    {
        if (startsRandom && Random.Range(0, 2) == 0)
        {
            startsUp = true;
        }
        else
        {
            startsUp = false;
        }

        LeanTween.moveY(gameObject, transform.position.y + ((startsUp ? 1 : -1) * (waveAmplitude/2)), waveFrequency/2).setEase(easeType).setOnComplete(() =>
        {
            LeanTween.moveY(gameObject, transform.position.y + ((startsUp ? -1 : 1) * waveAmplitude), waveFrequency).setEase(easeType).setLoopPingPong()
            .setOnUpdate((float _) => { 
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, Global.YRange.min, Global.YRange.max), transform.position.z); 
            });
        });
            
    }
}
