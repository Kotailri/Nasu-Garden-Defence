using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBoomerang : PlayerAttackPrefab
{
    public float rotationDuration;
    private bool isReturning = false;
    private bool hasLaunched = false;
    private Vector2 startingPosition = Vector2.zero;
    private float distance;
    private float speed;

    public override void Start()
    {
        base.Start();
        LeanTween.rotateAround(gameObject, new Vector3(0, 0, -1), 360f, rotationDuration).setLoopClamp();
    }

    public void Launch(Vector2 launchVector, float _distance)
    {
        transform.localScale *= GlobalPlayer.GetStatValue(PlayerStatEnum.projectileSize);
        hasLaunched = true;
        speed = launchVector.magnitude;
        distance = _distance;
        startingPosition = transform.position;
        GetComponent<Rigidbody2D>().velocity = launchVector;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasLaunched && isReturning && collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        if (hasLaunched && !isReturning)
        {
            if (Vector2.Distance((Vector2)transform.position, startingPosition) >= distance)
            {
                isReturning = true;
            }
        }

        if (isReturning)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = Vector2.MoveTowards(transform.position, Global.playerTransform.position, Time.deltaTime * speed);
        }
        base.Update();
    }
}
