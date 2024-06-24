using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public ProgressBar ShootCooldownBar;

    private float currentShootTimer = 0f;
    private List<PlayerAttack> attackList = new();
    private int attackCount = 1;

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
            attack.DoAttack(transform.position, attackCount);

            if (GlobalItemToggles.HasBwo)
            {
                if (!attack.IsFacingRequired || (attack.IsFacingRequired && Global.keystoneItemManager.IsBwoFacingAttackDirection))
                {
                    attack.DoAttack(GameObject.FindGameObjectWithTag("Bwo").transform.position, attackCount);
                }
            }
        }

        attackCount++;
        if (attackCount > 10)
            attackCount = 1;

        EventManager.TriggerEvent(EventStrings.PLAYER_SHOOT, null);
    }

    private void Update()
    {
        if (!Global.gameplayStarted) { return; }

        if (currentShootTimer >= PlayerScriptableSettings.ShootTimer)
        {
            currentShootTimer = 0;
            Shoot();
        }
        else
        {
            currentShootTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentShootTimer/PlayerScriptableSettings.ShootTimer));
    }
}
