using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBoomerangLauncher : PlayerAttack
{
    [Header("Purple Boomerang")]
    public float speed;
    public float distance;

    [Space(5f)]
    public float leftWeight = 1f;
    public float rightWeight = 3f;
    public float upWeight = 2f;
    public float downWeight = 2f;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        GameObject g = Instantiate(AttackPrefab, attackPosition, Quaternion.identity);
        g.GetComponent<PurpleBoomerang>().SetDamage(Mathf.RoundToInt(BaseDamage * GlobalStats.GetStatValue(PlayerStatEnum.damage) 
            * GlobalStats.GetStatValue(PlayerStatEnum.projectileDamage)));
        g.GetComponent<PurpleBoomerang>().SetKnockback(KnockbackAmount);
        g.GetComponent<PurpleBoomerang>().SetKnockbackTime(KnockbackTime);
        g.GetComponent<PlayerAttackPrefab>().SetKnockbackType(KnockbackType);
        g.GetComponent<PurpleBoomerang>().SetReturnTransform(attachObject);
        g.GetComponent<PurpleBoomerang>().Launch(GetRandomWeightedDirection() * speed * GlobalStats.GetStatValue(PlayerStatEnum.projectileSpeed), distance);
        
    }

    private Vector2 GetRandomWeightedDirection()
    {
        // Normalize weights
        float totalWeight = leftWeight + rightWeight + upWeight + downWeight;
        leftWeight /= totalWeight;
        rightWeight /= totalWeight;
        upWeight /= totalWeight;
        downWeight /= totalWeight;

        float randomValue = Random.value;
        float angle;

        if (randomValue < leftWeight)
        {
            angle = Random.Range(135f, 225f);  // Left
        }
        else if (randomValue < leftWeight + rightWeight)
        {
            angle = Random.Range(-45f, 45f);  // Right
        }
        else if (randomValue < leftWeight + rightWeight + upWeight)
        {
            angle = Random.Range(45f, 135f);  // Up
        }
        else
        {
            angle = Random.Range(-135f, -45f);  // Down
        }

        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
    }
}
