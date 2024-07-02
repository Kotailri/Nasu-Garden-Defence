using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenHealth : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    public ProgressBarWithText gardenHealthUI;

    private float currentRegenAmount = 0f;

    private void Start()
    {
        MaxHP = (int)GlobalPlayer.GetStatValue(PlayerStatEnum.gardenHealth);
        CurrentHP = MaxHP;
        UpdateUI(CurrentHP);
    }

    private void Update()
    {
        if (!Global.waveManager.IsWaveOngoing())
        {
            return;
        }

        currentRegenAmount += GlobalPlayer.GetStatValue(PlayerStatEnum.gardenRegen) * Time.deltaTime;
        if (currentRegenAmount >= 1)
        {
            SetHealth(Mathf.FloorToInt(currentRegenAmount), true);
            currentRegenAmount %= 1;
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.STATS_UPDATED, OnMaxHpStatChanged);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.STATS_UPDATED, OnMaxHpStatChanged);
    }

    public int GetHealth()
    {
        return CurrentHP;
    }

    public int GetMaxHealth()
    {
        return MaxHP;
    }

    public void SetHealth(int _hp, bool isRelative)
    {
        if (isRelative)
        {
            if (_hp >= 0)
            {
                CurrentHP += _hp;
            }
            else
            {
                CurrentHP += (int)Mathf.Clamp(_hp + Mathf.FloorToInt(Mathf.Abs(_hp) * GlobalPlayer.GetStatValue(PlayerStatEnum.gardenResist)), Mathf.NegativeInfinity, 0f);
            }
            
        }
        else
        {
            CurrentHP = _hp;
        }
        
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        CheckDeath();
        UpdateUI(CurrentHP);
    }

    public void OnMaxHpStatChanged(Dictionary<string, object> message)
    {
        SetMaxHealth(Mathf.FloorToInt(GlobalPlayer.GetStatValue(PlayerStatEnum.gardenHealth)) - MaxHP, true);
        UpdateUI(CurrentHP);
    }

    public void SetMaxHealth(int _hp, bool isRelative)
    {
        if (isRelative)
        {
            CurrentHP += _hp;
            MaxHP += _hp;
        }
        else
        {
            CurrentHP = _hp;
            MaxHP = _hp;
        }

        UpdateUI(CurrentHP);
    }

    private void UpdateUI(int CurrentHP)
    {
        gardenHealthUI.UpdateValue((float)CurrentHP / (float)MaxHP);
        gardenHealthUI.UpdateText(CurrentHP);
    }

    private void CheckDeath()
    {
        if (CurrentHP <= 0)
        {
            Global.GameOver();
        }
    }
}
