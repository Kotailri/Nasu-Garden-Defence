using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
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

    [Space(10f)]
    public int NumberOfProjectiles;
    public float DelayBetweenProjectiles;

    [Header("Timing")]
    public EnemyShootTimerType TimerType;
    public float ShootTimer;
    public int AnimationFrame;
    public int AnimationFrameOffset;

    private float currentShootTimer = 0f;
    private bool didShootOnCurrentFrame = false;
    private int currentAnimFrameOffset = 0;

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
        if (!GameUtil.IsObjectActiveOnField(this.gameObject, true, false))
        {
            return;
        }

        switch (TimerType)
        {
            case EnemyShootTimerType.ShootsOnAnimationFrame:
                if (GameUtil.GetCurrentAnimationFrame(GetComponent<Animator>()) == AnimationFrame)
                {
                    if (didShootOnCurrentFrame == false)
                    {
                        if (currentAnimFrameOffset >= AnimationFrameOffset)
                        {
                            Shoot();
                            currentAnimFrameOffset = 0;
                        }

                        currentAnimFrameOffset++;
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
                    Shoot();
                    currentShootTimer = 0;
                }
                break;
        }
    }

    public virtual void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    public virtual void ShootProjectile()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, ShootPosition.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * ProjectileDirection.normalized;
    }

    protected virtual IEnumerator ShootCoroutine()
    {
        for(int i = 0; i < NumberOfProjectiles; i++)
        {
            ShootProjectile();
            yield return new WaitForSeconds(DelayBetweenProjectiles);
        }
    }

}
