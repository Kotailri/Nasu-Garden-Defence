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
        Global.damageTextSpawner.SpawnText(transform.position, heal.ToString(), DamageTextType.Green, 1f);
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
            if (Random.Range(0f,1f) < GlobalPlayer.GetStatValue(PlayerStatEnum.critchance))
            {
                // TODO crit sound effect
                CurrentHealth -= (damage - Mathf.FloorToInt(damage * Resistance))*2;
                Global.damageTextSpawner.SpawnText(textSpawnLocation, ((damage - Mathf.FloorToInt(damage * Resistance)) * 2).ToString(), DamageTextType.Crit, 1f);
            }
            else
            {
                CurrentHealth -= damage - Mathf.FloorToInt(damage * Resistance);
                Global.damageTextSpawner.SpawnText(textSpawnLocation, (damage - Mathf.FloorToInt(damage * Resistance)).ToString(), DamageTextType.White, 1f);
            }

            if (canGetExecuted && CurrentHealth > 0 && Global.itemPassiveManager.GetPassive(ItemPassiveEnum.LowHealthExecute) && (float)CurrentHealth/(float)Health <= Global.itemPassiveManager.LowHealthExecutePercent)
            {
                // TODO execute sound + effect
                CurrentHealth -= 99999;
                Global.damageTextSpawner.SpawnText(textSpawnLocation, "99999", DamageTextType.White, 1f);
            }
        }
        else
        {
            Global.damageTextSpawner.SpawnText(textSpawnLocation, "dodged", DamageTextType.Status, 1f);
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
