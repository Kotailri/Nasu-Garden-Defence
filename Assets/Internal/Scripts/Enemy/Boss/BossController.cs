using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossScriptable bossScriptable;
    public int GardenContactDamage;

    public DamagesPlayerOnHit _contactDamageComponent;
    public BossHealth _healthComponent;

    [Space(5f)]
    public float BossActivePosition;

    private void Awake()
    {
        _healthComponent = gameObject.AddComponent<BossHealth>();
        _healthComponent.SetEnemyHealth(bossScriptable.Health, bossScriptable.Resistance, bossScriptable.DodgeChance, bossScriptable.HealthRegen);

        _contactDamageComponent = gameObject.AddComponent<DamagesPlayerOnHit>();
        _contactDamageComponent.SetDamage(bossScriptable.ContactDamage);

        GardenContactDamage = bossScriptable.GardenContactDamage;

        if (TryGetComponent(out EnemyGetHit _) == false)
            gameObject.AddComponent<EnemyGetHit>();
        
    }

    private void Start()
    {
        Managers.Instance.Resolve<IBossHealthBarMng>().SetBossName(bossScriptable.BossName);
    }

    private void Update()
    {
        if (!Managers.Instance.Resolve<IBossHealthBarMng>().IsBossHealthActive() && transform.position.x <= BossActivePosition)
        {
            Managers.Instance.Resolve<IBossHealthBarMng>().ActivateHealthBar(1.5f);
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
        Managers.Instance.Resolve<IBossHealthBarMng>().DeactivateHealthBar();
        EventManager.TriggerEvent(EventStrings.ENEMY_DELETED, null);
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
