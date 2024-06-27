using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public ProgressBar ShootCooldownBar;
    public float AttackTimer = 2f;

    private float currentAttackTimer = 0f;
    private List<PlayerAttack> attackList = new();

    public void RefreshAttackList()
    {
        attackList = new(GetComponents<PlayerAttack>());
    }

    private void Start()
    {
        RefreshAttackList();
    }

    private void Shoot()
    {
        foreach (PlayerAttack attack in attackList)
        {
            attack.DoAttack(transform.position);

            if (GlobalItemToggles.HasBwo)
            {
                if (!attack.IsPetFacingRequired || (attack.IsPetFacingRequired && Global.keystoneItemManager.IsBwoFacingAttackDirection))
                {
                    attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position);
                }
            }
        }

        EventManager.TriggerEvent(EventStrings.PLAYER_ATTACK, null);
    }

    private void Update()
    {
        if (!Global.gameplayStarted) { return; }

        if (currentAttackTimer >= AttackTimer)
        {
            currentAttackTimer = 0;
            Shoot();
        }
        else
        {
            currentAttackTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentAttackTimer/AttackTimer));
    }
}
