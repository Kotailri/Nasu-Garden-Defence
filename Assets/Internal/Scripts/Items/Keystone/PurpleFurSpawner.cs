using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PurpleFurSpawner : MonoBehaviour
{
    public GameObject FurPrefab;
    public float ShedTimer;

    [Space(5f)]
    public float FurDuration;
    public float SlowAmount;
    public float DamageTimer;
    public int Damage;

    private bool startSpawning = false;
    private float currentTimer = 0f;
    private Rigidbody2D RB;

    public void Initialize(GameObject _prefab, float _shedTimer, float _furDuration, float _slowAmount, float _damageTimer, int _damage)
    {
        FurPrefab = _prefab;

        ShedTimer = _shedTimer;
        FurDuration = _furDuration;
        SlowAmount = _slowAmount;
        DamageTimer = _damageTimer;
        Damage = _damage;

        RB = GetComponent<Rigidbody2D>();

        startSpawning = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!startSpawning)
        {
            return;
        }

        currentTimer += Time.deltaTime;

        if (currentTimer > ShedTimer)
        {
            Vector2 spawnDirection = -RB.velocity.normalized;

            GameObject currentFur = Instantiate(FurPrefab, transform.position + ((Vector3)spawnDirection * 1.1f), Quaternion.identity);
            currentFur.GetComponent<PurpleFur>().SetParams(FurDuration, SlowAmount, DamageTimer, Damage);
            currentTimer = 0;
        }
    }
}
