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

        if (TryGetComponent(out DamagesPlayerOnHit damagesPlayer))
        {
            _contactDamageComponent = damagesPlayer;
        }
        else
        {
            _contactDamageComponent = gameObject.AddComponent<DamagesPlayerOnHit>();
        }
        
        _contactDamageComponent.SetDamage(enemyScriptable.ContactDamage);

        GardenContactDamage = enemyScriptable.GardenContactDamage;

        if (!TryGetComponent(out EnemyGetHit _))
        {
            gameObject.AddComponent<EnemyGetHit>();
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.GAME_OVER_KILL_ALL, DestroyFromGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.GAME_OVER_KILL_ALL, DestroyFromGameOver);
    }

    private void OnDestroy()
    {
        EventManager.TriggerEvent(EventStrings.ENEMY_DELETED, null);
    }

    private void DestroyFromGameOver(Dictionary<string, object> msg)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, Global.YRange.min, Global.YRange.max), transform.position.z);
    }
}
