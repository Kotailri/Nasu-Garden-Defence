using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager { }

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance => _instance;
    public List<GameObject> preloadedManagers = new();
    public List<GameObject> managerObjects = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // Register all dependencies
        RegisterDependencies();
    }

    private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public void Register<T>(T service)
    {
        var type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services[type] = service;
        }
    }

    public T Resolve<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }

        throw new Exception($"Service of type {type} not registered.");
    }

    private void RegisterDependencies()
    {
        managerObjects.Clear();
        foreach (Transform child in transform)
        {
            managerObjects.Add(child.gameObject);
        }

        foreach (GameObject gm in preloadedManagers)
        {
            managerObjects.Add(gm);
        }

        Register<ITextSpawnerMng>(managerObjects.FindComponentInList<TextSpawner>());
        Register<IGameOverMng>(managerObjects.FindComponentInList<GameOverManager>());
        Register<IWaveMng>(managerObjects.FindComponentInList<WaveManager>());
        Register<IItemSelectMng>(managerObjects.FindComponentInList<ItemSelectManager>());
        Register<IItemInventoryMng>(managerObjects.FindComponentInList<ItemInventoryManager>());
        Register<IBossHealthBarMng>(managerObjects.FindComponentInList<BossHealthBarManager>());
        Register<IGardenBuffMng>(managerObjects.FindComponentInList<GardenBuffManager>());
        Register<IPrefabMng>(managerObjects.FindComponentInList<PrefabManager>());
        Register<IAlertMng>(managerObjects.FindComponentInList<AlertManager>());
        Register<IAttackPipelineMng>(managerObjects.FindComponentInList<DamagePipelineManager>());
    }

    
}
