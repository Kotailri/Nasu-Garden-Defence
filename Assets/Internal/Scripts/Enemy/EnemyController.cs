using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptable enemyScriptable;
    public ProgressBar healthBar;
    public int GardenContactDamage;

    protected EnemyHealth _healthComponent;
    protected DamagesPlayerOnHit _contactDamageComponent;

    protected void Awake()
    {
        _healthComponent = gameObject.AddComponent<EnemyHealth>();
        _healthComponent.SetEnemyHealth(enemyScriptable.Health, enemyScriptable.Resistance, enemyScriptable.DodgeChance, enemyScriptable.HealthRegen);
        if (healthBar != null )
            _healthComponent.SetHealthBar(healthBar);

        _contactDamageComponent = gameObject.AddComponent<DamagesPlayerOnHit>();
        _contactDamageComponent.SetDamage(enemyScriptable.ContactDamage);

        GardenContactDamage = enemyScriptable.GardenContactDamage;

        gameObject.AddComponent<EnemyGetHit>();
        gameObject.AddComponent<CallsTriggerCollisions>();

        if (TryGetComponent(out Animator animator))
            animator.speed = 0f;
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.GAME_OVER, DestroyFromGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.GAME_OVER, DestroyFromGameOver);
    }

    private void DestroyFromGameOver(Dictionary<string, object> msg)
    {
        Destroy(gameObject);
    }

    public bool IsEnemyActive()
    {
        return transform.position.x < 18.5f;
    }
}
