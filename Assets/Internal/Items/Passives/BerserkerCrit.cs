using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerCrit : MonoBehaviour
{
    public float missingHealthPerCritPercent;
    private PlayerHealth playerHealth;

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.PLAYER_HEALTH_UPDATED, UpdateCrit);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.PLAYER_HEALTH_UPDATED, UpdateCrit);
    }

    private float currentCritBoost = 0f;
    private void UpdateCrit(Dictionary<string, object> _)
    {
        if (!playerHealth)
        {
            playerHealth = Global.playerTransform.gameObject.GetComponent<PlayerHealth>();
        }

        int missingHealth = playerHealth.GetMaxHealth() - playerHealth.GetHealth();
        float percentCritIncrease = (float)missingHealth / missingHealthPerCritPercent;
        percentCritIncrease /= 100f;

        percentCritIncrease = Mathf.Round(percentCritIncrease * 100.0f) / 100f;

        GlobalPlayer.GetStat(PlayerStatEnum.critchance).RemoveStatAdditive(currentCritBoost);
        GlobalPlayer.GetStat(PlayerStatEnum.critchance).AddStatAdditive(percentCritIncrease);

        currentCritBoost = percentCritIncrease;
    }
}
