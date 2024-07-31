using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NasuTornado : PlayerAttackPrefab
{
    private bool hasLaunched = false;
    private Vector2 startingPosition = Vector2.zero;
    private float distance;
    private float speed;

    public void Launch(Vector2 launchVector, float _distance)
    {
        transform.localScale *= GlobalStats.GetStatValue(PlayerStatEnum.attackSize);
        hasLaunched = true;
        speed = launchVector.magnitude;
        distance = _distance;
        startingPosition = transform.position;
        GetComponent<Rigidbody2D>().velocity = launchVector.normalized * speed;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage++;
        }
        base.OnTriggerEnter2D(collision);
    }

    protected override void Update()
    {
        if (hasLaunched)
        {
            if (Vector2.Distance((Vector2)transform.position, startingPosition) >= distance)
            {
                LeanTween.alpha(gameObject, 0, 0.15f).setOnComplete(() => { Destroy(gameObject); });
            }
        }

        base.Update();
    }
}
