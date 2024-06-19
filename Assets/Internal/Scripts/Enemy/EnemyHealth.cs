using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    private int   Health;
    private float Resistance;
    private float DodgeChance;
    private int   HealthRegenPerSecond;

    private int CurrentHealth;

    public void SetEnemyHealth(int _health, float _resistPercent, float _dodgeChance, int _healthRegenPerSecond)
    {
        Health = _health;
        Resistance = _resistPercent;
        DodgeChance = _dodgeChance;
        HealthRegenPerSecond = _healthRegenPerSecond;

        CurrentHealth = _health;
        InvokeRepeating(nameof(RegenHealth), 0, 1);
    }

    public void Heal(int heal)
    {
        CurrentHealth += heal;

        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        if (Random.Range(0f, 1f) < DodgeChance)
            CurrentHealth -= Mathf.FloorToInt(damage * Resistance);

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }

        if (CurrentHealth <= 0)
        {
            GetComponent<EnemyDeath>().Die();
        }
    }

    private void RegenHealth()
    {
        Heal(HealthRegenPerSecond);
    }
}
