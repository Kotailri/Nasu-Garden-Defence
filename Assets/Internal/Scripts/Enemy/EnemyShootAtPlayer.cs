using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAtPlayer : EnemyShoot
{
    Vector2 aimPosition;
    public override void Shoot()
    {
        Vector2 displacement = (Vector2)Global.playerTransform.position - (Vector2)transform.position;
        float timeToReachPlayer = displacement.magnitude / ProjectileSpeed;
        aimPosition = (Vector2)Global.playerTransform.position + Global.playerMoveVector * timeToReachPlayer;

        base.Shoot();
    }

    public override void ShootProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, ShootPosition.transform.position, Quaternion.identity);
        Vector2 shootDirection = (aimPosition + (new Vector2(Random.Range(-ProjectileDirection.x, ProjectileDirection.x), Random.Range(-ProjectileDirection.y, ProjectileDirection.y))).normalized) - (Vector2)transform.position;
        shootDirection.Normalize();

        projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * shootDirection.normalized;
    }
}
