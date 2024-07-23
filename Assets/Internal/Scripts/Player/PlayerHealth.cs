using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    [Space(10f)]
    public ProgressBar bar;
    public CanvasGroup vignette;

    private float currentRegenAmount = 0f;

    private void Start()
    {
        MaxHP = (int)GlobalPlayer.GetStatValue(PlayerStatEnum.playerHealth);
        CurrentHP = MaxHP;
        UpdateUI(CurrentHP);

        vignette.alpha = 0f;
    }

    private void FlashVignette()
    {
        if (vignette == null) return;

        LeanTween.cancel(vignette.gameObject);

        vignette.alpha = 0f;
        LeanTween.alphaCanvas(vignette, 1f, 0.1f).setEaseInExpo().setOnComplete(() => 
        {
            LeanTween.alphaCanvas(vignette, 0f, 0.75f);
        });
    }

    private void Update()
    {
        if (!Global.waveManager.IsWaveOngoing())
        {
            return;
        }

        currentRegenAmount += GlobalGarden.PlayerRegeneration * Time.deltaTime;
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

    public float GetHealthPercent()
    {
        return (float)CurrentHP / (float)MaxHP;
    }

    public void FullHeal()
    {
        CurrentHP = MaxHP;
        UpdateUI(CurrentHP);
    }

    public void HealPercent(float percent)
    {
        SetHealth(Mathf.CeilToInt(percent * MaxHP), true);
    }

    public void SetHealth(int _hp, bool isRelative)
    {
        if (isRelative)
        {
            if (_hp > 0)
            {
                CurrentHP += _hp;
                Global.damageTextSpawner.SpawnText(transform.position, _hp.ToString(), DamageTextType.Green, 1f);
            }
            else if (_hp < 0)
            {
                int damage = _hp;
                CurrentHP += damage;
                FlashVignette();
                AudioManager.instance.PlaySound(AudioEnum.PlayerDamaged);
                AudioManager.instance.PlaySound(AudioEnum.UhOh);
                Global.damageTextSpawner.SpawnText(transform.position, "-" + Mathf.FloorToInt(Mathf.Abs(damage)).ToString(), DamageTextType.Red, 1f);
            }
            else
            {
                return;
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
        SetMaxHealth(Mathf.FloorToInt(GlobalPlayer.GetStatValue(PlayerStatEnum.playerHealth)) - MaxHP, true);
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
        EventManager.TriggerEvent(EventStrings.PLAYER_HEALTH_UPDATED, null);
        bar.UpdateValue((float)CurrentHP / (float)MaxHP);
    }

    private void CheckDeath()
    {
        if (CurrentHP <= 0)
        {
            Global.GameOver(DeathCondition.PlayerDeath);
        }
    }
}
