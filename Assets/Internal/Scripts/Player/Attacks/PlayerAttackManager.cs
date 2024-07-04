using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public ProgressBar ShootCooldownBar;
    public float AttackTimer = 2f;

    private float currentAttackTimer = 0f;
    public List<PlayerAttack> attackList = new();

    public void RefreshAttackList()
    {
        attackList = new(GetComponentsInChildren<PlayerAttack>(false));
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
            if (attack.IsOnAttackTimer && count % attack.AttackCount != 0)
            {
                continue;
            }

            attack.DoAttack(transform.position);

            if (GlobalItemToggles.HasBwo)
            {
                if (!attack.IsPetFacingRequired || (attack.IsPetFacingRequired && Global.keystoneItemManager.IsBwoFacingAttackDirection))
                {
                    if (attack.IsAttached)
                    {
                        attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position, GameObject.FindGameObjectWithTag("Bwo").transform);
                    }
                    else
                    {
                        attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position);
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

        if (currentAttackTimer >= (AttackTimer - (AttackTimer * GlobalPlayer.GetStatValue(PlayerStatEnum.attackspeed))))
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
