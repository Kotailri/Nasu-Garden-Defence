using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShootTimerType
{
    ShootsOnAnimationFrame,
    ShootsOnTimer
}

public class EnemyShoot : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform ShootPosition;

    [Header("Shooting")]
    public Vector2 ProjectileDirection;
    public float ProjectileSpeed;

    [Header("Timing")]
    public EnemyShootTimerType TimerType;
    public float ShootTimer;
    public int AnimationFrame;

    private float currentShootTimer = 0f;
    private bool didShootOnCurrentFrame = false;

    private void Awake()
    {
        if (ProjectilePrefab.TryGetComponent(out Rigidbody2D _) == false)
        {
            print(ProjectilePrefab.name + " enemy projectile has no Rigidbody2D component");
        }

        currentShootTimer = ShootTimer / 2f;
    }

    protected virtual void Update()
    {
        if (!Global.IsObjectActiveOnField(this.gameObject, true, false))
        {
            return;
        }

        switch (TimerType)
        {
            case EnemyShootTimerType.ShootsOnAnimationFrame:
                if (Global.GetCurrentAnimationFrame(GetComponent<Animator>()) == AnimationFrame)
                {
                    if (didShootOnCurrentFrame == false)
                    {
                        ShootProjectile();
                        didShootOnCurrentFrame = true;
                    }
                }
                else
                {
                    didShootOnCurrentFrame = false;
                }
                break;

            case EnemyShootTimerType.ShootsOnTimer:
                currentShootTimer += Time.deltaTime;

                if (currentShootTimer >= ShootTimer)
                {
                    ShootProjectile();
                    currentShootTimer = 0;
                }
                break;
        }
    }

    public virtual void ShootProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, ShootPosition.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * ProjectileDirection.normalized;
    }

}
