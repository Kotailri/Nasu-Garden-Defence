using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public ProgressBar ShootCooldownBar;

    private float currentShootTimer = 0f;

    public void SetProjectilePrefab(GameObject prefab)
    {
        ProjectilePrefab = prefab;
    }
    private void Start()
    {
        SetProjectilePrefab(PlayerScriptableSettings.ProjectilePrefab);
    }

    private void ShootProjectile(Vector2 startPos)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, startPos + new Vector2(0.25f, -0.25f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerScriptableSettings.ProjectileSpeed + PlayerScriptableSettings.PlayerMovespeed, 0);
    }

    private void Shoot()
    {
        ShootProjectile(transform.position);
        if (GlobalItemToggles.HasBwo && Global.keystoneItemManager.CanBwoShoot)
        {
            ShootProjectile(GameObject.FindGameObjectWithTag("Bwo").transform.position);
        }

        EventManager.TriggerEvent(EventStrings.PLAYER_SHOOT, null);
    }

    private void Update()
    {
        if (!Global.gameplayStarted) { return; }

        if (currentShootTimer >= PlayerScriptableSettings.ShootTimer)
        {
            currentShootTimer = 0;
            Shoot();
        }
        else
        {
            currentShootTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentShootTimer/PlayerScriptableSettings.ShootTimer));
    }
}
