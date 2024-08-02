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

    int count = 1;
    private void Attack()
    {
        foreach (PlayerAttack attack in attackList)
        {
            if (!attack.IsOnAttackTimer 
                || (!attack.OnOffset && count % attack.AttackCount == 0)
                || (attack.OnOffset && count % attack.AttackCount == 1))
            {

                attack.DoAttack(transform.position, transform);

            }
            
        }

        foreach (PlayerAttack attack in attackListBwo) 
        {
            if (!attack.IsOnAttackTimer
                || (!attack.OnOffset && (count+1) % attack.AttackCount == 0)
                || (attack.OnOffset && (count+1) % attack.AttackCount == 1))
            { 

                if (GlobalItemToggles.HasBwo)
                {
                    attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position, GameObject.FindGameObjectWithTag("Bwo").transform);
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
        if (!Global.gameplayStarted || !GameUtil.IsWaveOngoing()) { return; }

        if (currentAttackTimer >= GetAttackTimer())
        {
            currentAttackTimer = 0;
            Attack();
        }
        else
        {
            currentAttackTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentAttackTimer/ GetAttackTimer()));
    }

    private float GetAttackTimer()
    {
        return AttackTimer - Mathf.Clamp(AttackTimer * GlobalStats.GetStatValue(PlayerStatEnum.attackspeed), 0, (AttackTimer * 0.99f));
    }
}
