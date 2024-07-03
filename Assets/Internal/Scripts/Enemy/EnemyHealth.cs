using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    private int   Health;
    private float Resistance;
    private float DodgeChance;
    private int   HealthRegen;

    public int CurrentHealth;

    private float currentRegenAmount = 0f;
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

    public void TakeDamage(int damage)
    {
        if (Random.Range(0f, 1f) >= DodgeChance)
        {
            CurrentHealth -= damage - Mathf.FloorToInt(damage * Resistance);
            Global.damageTextSpawner.SpawnText(transform.position, (damage - Mathf.FloorToInt(damage * Resistance)).ToString(), DamageTextType.White, 1f);
        }
        else
        {
            Global.damageTextSpawner.SpawnText(transform.position, "dodged", DamageTextType.Status, 1f);
        }
            

        CheckHealth();
    }

    private void CheckHealth()
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
