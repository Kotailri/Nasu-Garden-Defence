using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    protected int   Health;
    protected float Resistance;
    protected float DodgeChance;
    protected int   HealthRegen;

    public int CurrentHealth;

    protected float currentRegenAmount = 0f;
    private ProgressBar healthBar;

    public void SetHealthBar(ProgressBar bar)
    {
        healthBar = bar;
    }

    public void SetEnemyHealth(int _health, float _resistPercent, float _dodgeChance, int _healthRegenPerSecond)
    {
        Health = _health;
        Resistance = _resistPercent;
        DodgeChance = _dodgeChance;
        HealthRegen = _healthRegenPerSecond;

        CurrentHealth = _health;
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;
        Global.damageTextSpawner.SpawnText(transform.position, heal.ToString(), DamageTextType.Green, 1f);
        CheckHealth();
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage == 0)
        {
            return;
        }

        if (Random.Range(0f, 1f) >= DodgeChance)
        {
            CurrentHealth -= damage - Mathf.FloorToInt(damage * Resistance);
            Global.damageTextSpawner.SpawnText(transform.position, (damage - Mathf.FloorToInt(damage * Resistance)).ToString(), DamageTextType.White, 1f);

            if (CurrentHealth > 0 && Global.itemPassiveManager.GetPassive(ItemPassiveEnum.LowHealthExecute) && (float)CurrentHealth/(float)Health <= Global.itemPassiveManager.LowHealthExecutePercent)
            {
                CurrentHealth -= 99999;
                Global.damageTextSpawner.SpawnText(transform.position, "99999", DamageTextType.White, 1f);
            }
        }
        else
        {
            Global.damageTextSpawner.SpawnText(transform.position, "dodged", DamageTextType.Status, 1f);
        }
            

        CheckHealth();
    }

    protected virtual void CheckHealth()
    {
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }

        healthBar?.UpdateValue((float)CurrentHealth/(float)Health);

        if (CurrentHealth <= 0)
        {
            GetComponent<EnemyDeath>().Die();
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
