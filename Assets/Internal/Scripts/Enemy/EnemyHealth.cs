using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    protected int   Health;
    protected float Resistance;
    protected float DodgeChance;
    protected float   HealthRegen;
    protected bool canGetExecuted = true;

    public int CurrentHealth;

    protected float currentRegenAmount = 0f;
    private ProgressBar healthBar;

    private ITextSpawnerMng TextSpawnerMng;

    protected virtual void Awake()
    {
        TextSpawnerMng = Managers.Instance.Resolve<ITextSpawnerMng>();
    }

    public void SetHealthBar(ProgressBar bar)
    {
        healthBar = bar;
    }

    public void SetEnemyHealth(int _health, float _resistPercent, float _dodgeChance, float _healthRegenPerSecond)
    {
        Health = Mathf.CeilToInt(_health * Global.EnemyHealthMultiplier);
        Resistance = _resistPercent;
        DodgeChance = _dodgeChance;
        HealthRegen = _healthRegenPerSecond;

        CurrentHealth = Health;
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        TextSpawnerMng.SpawnText(transform.position, heal.ToString(), DamageTextType.Green, 1f);
        CheckHealth();
    }

    public virtual void TakeDamage(int damage, Vector2 location)
    {
        if (damage == 0)
        {
            return;
        }

        Vector2 textSpawnLocation = (Vector2)transform.position + (location - (Vector2)transform.position).normalized;

        if (Random.Range(0f, 1f) >= DodgeChance)
        {
            if (Random.Range(0f,1f) < GlobalStats.GetStatValue(PlayerStatEnum.critchance))
            {
                AudioManager.instance.PlaySound(AudioEnum.CritSound);
                CurrentHealth -= (damage - Mathf.FloorToInt(damage * Resistance))*2;
                TextSpawnerMng.SpawnText(textSpawnLocation, ((damage - Mathf.FloorToInt(damage * Resistance)) * 2).ToString(), DamageTextType.Crit, 1f);
            }
            else
            {
                CurrentHealth -= damage - Mathf.FloorToInt(damage * Resistance);
                TextSpawnerMng.SpawnText(textSpawnLocation, (damage - Mathf.FloorToInt(damage * Resistance)).ToString(), DamageTextType.White, 1f);
            }

            if (canGetExecuted && CurrentHealth > 0 && Global.itemPassiveManager.GetPassive(ItemPassiveEnum.LowHealthExecute) && (float)CurrentHealth/(float)Health <= Global.itemPassiveManager.LowHealthExecutePercent)
            {
                AudioManager.instance.PlaySound(AudioEnum.ExecuteSound);
                GameObject effect = Managers.Instance.Resolve<IPrefabMng>().InstantiatePrefab(PrefabEnum.ExecuteEffect, transform.position, Quaternion.identity);
                Vector3 originalScale = effect.transform.localScale;
                effect.transform.SetParent(transform, false);
                effect.transform.localScale = VectorUtil.DivideVector3(originalScale, transform.lossyScale);
                effect.transform.localPosition = Vector3.zero;

                CurrentHealth -= 99999;
                TextSpawnerMng.SpawnText(textSpawnLocation, "99999", DamageTextType.White, 1f);
            }
        }
        else
        {
            TextSpawnerMng.SpawnText(textSpawnLocation, "dodged", DamageTextType.Status, 1f);
        }
            

        CheckHealth(location);
    }

    protected virtual void CheckHealth(Vector2? location=null)
    {
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }

        healthBar?.UpdateValue((float)CurrentHealth/(float)Health);

        if (CurrentHealth <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EnemyDeath>().Die(location);
        }
    }

    private void Update()
    {
        currentRegenAmount += HealthRegen * Time.deltaTime;
        if (currentRegenAmount >= 1)
        {
            Heal(Mathf.FloorToInt(currentRegenAmount));
            currentRegenAmount %= 1;
        }
    }
}
