using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSlugKing : Boss
{
    public float speed;

    [Space(5f)]
    public float timeBetweenStops;
    public float stopTime;

    [Space(5f)]
    public GameObject miniSlug;
    public float TimeBetweenSpawning;

    [Space(5f)]
    public GameObject slugSpit;

    private void Start()
    {
        StartMoving();
        StartCoroutine(SpawningCoroutine());
        StartCoroutine(DoAttackCoroutine());
    }

    private void StartMoving()
    {
        RB.velocity = new Vector2(-speed, 0);
    }

    private void StopMoving()
    {
        RB.velocity = Vector2.zero;
    }

    private IEnumerator DoAttack()
    {
        Instantiate(slugSpit, GetSpitLocation(0.5f), slugSpit.transform.rotation);
        yield return new WaitForSeconds(0.25f);
        Instantiate(slugSpit, GetSpitLocation(1f), slugSpit.transform.rotation);
        yield return new WaitForSeconds(0.25f);
        Instantiate(slugSpit, GetSpitLocation(1.5f), slugSpit.transform.rotation);
    }

    private Vector2 GetSpitLocation(float variance)
    {
        float playerX = Global.playerTransform.position.x;
        float playerY = Global.playerTransform.position.y;

        float randomX = Mathf.Clamp(Random.Range(playerX - variance, playerX + variance), Global.XRange.min, Global.XRange.max);
        float randomY = Mathf.Clamp(Random.Range(playerY - variance, playerY + variance), Global.YRange.min, Global.YRange.max);

        return new Vector2(randomX, randomY);
    }

    private IEnumerator SpawningCoroutine()
    {
        yield return new WaitForSeconds(TimeBetweenSpawning);
        Instantiate(miniSlug, new Vector3(19f, 6f, 0), Quaternion.identity);
        Instantiate(miniSlug, new Vector3(19f, -6f, 0), Quaternion.identity);
        StartCoroutine(SpawningCoroutine());
    }

    private IEnumerator DoAttackCoroutine()
    {
        yield return new WaitUntil(() => IsInActiveRange());

        StopMoving();

        StartCoroutine(DoAttack());
        yield return new WaitForSeconds(stopTime);

        StartMoving();
        yield return new WaitForSeconds(timeBetweenStops);

        StartCoroutine(DoAttackCoroutine());
    }
}
