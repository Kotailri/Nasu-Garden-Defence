using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public ProgressBar ShootCooldownBar;
    public float AttackTimer = 2f;

    private float currentAttackTimer = 0f;
    public List<PlayerAttack> attackList = new();
    public List<PlayerAttack> attackListBwo = new();

    public void RefreshAttackList()
    {
        attackList = new(GetComponentsInChildren<PlayerAttack>(true));
        if (GameObject.FindGameObjectWithTag("Bwo") != null)
        {
            attackListBwo = new(GameObject.FindGameObjectWithTag("Bwo").GetComponentsInChildren<PlayerAttack>(true));
        }
        
        /*bool onOffset = false;
        foreach (PlayerAttack attack in attackList)
        {
            if (attack.AttackCount == 2)
            {
                attack.OnOffset = onOffset;
                onOffset = !onOffset;
            }
        }*/
    }

    private void Start()
    {
        RefreshAttackList();
    }

    private float currentBuffedSpeed = 0f;
    public void AddTempAttackSpeed(float multiplier, float time)
    {
        float newBuffedSpeed = multiplier * AttackTimer;

        if (newBuffedSpeed > currentBuffedSpeed)
        {
            StopAllCoroutines();
            currentBuffedSpeed = multiplier * AttackTimer;
            StartCoroutine(BuffSpeed(time));
        }
        
    }

    private IEnumerator BuffSpeed(float timer)
    {
        yield return new WaitForSeconds(timer);
        currentBuffedSpeed = 0;
    }

    int count = 1;
    private void Attack()
    {
        foreach (PlayerAttack attack in attackList)
        {
            if (!attack.IsOnAttackTimer 
                || (!attack.OnOffset && count % attack.AttackCount == 0)
                || (attack.OnOffset && count % attack.AttackCount == 1))
            {

                attack.DoAttack(transform.position);

            }
            
        }

        foreach (PlayerAttack attack in attackListBwo) 
        {
            if (!attack.IsOnAttackTimer
                || (!attack.OnOffset && count % attack.AttackCount == 0)
                || (attack.OnOffset && count % attack.AttackCount == 1))
            { 

                if (GlobalItemToggles.HasBwo)
                {
                    if (!attack.IsPetFacingRequired || (attack.IsPetFacingRequired && Global.keystoneItemManager.IsBwoFacingAttackDirection))
                    {
                        attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position, GameObject.FindGameObjectWithTag("Bwo").transform);
                        attack.DoAttack(transform.position);
                    }
                }

            }
        }

        count++;
        if (count == 11)
            count = 1;

        EventManager.TriggerEvent(EventStrings.PLAYER_ATTACK, null);
    }

    private void Update()
    {
        if (!Global.gameplayStarted || !Global.waveManager.IsWaveOngoing()) { return; }

        if (currentAttackTimer >= (AttackTimer - (AttackTimer * GlobalPlayer.GetStatValue(PlayerStatEnum.attackspeed)) - currentBuffedSpeed))
        {
            currentAttackTimer = 0;
            Attack();
        }
        else
        {
            currentAttackTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentAttackTimer/AttackTimer));
    }
}
