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

    private void Update()
    {
        
        if (currentShootTimer >= PlayerScriptableSettings.ShootTimer)
        {
            currentShootTimer = 0;
            GameObject projectile = Instantiate(ProjectilePrefab, transform.position + new Vector3(0.25f,0,0), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerScriptableSettings.ProjectileSpeed + PlayerScriptableSettings.PlayerMovespeed, 0);
        }
        else
        {
            currentShootTimer += Time.deltaTime;
        }

        ShootCooldownBar.UpdateValue(Mathf.Clamp01(currentShootTimer/PlayerScriptableSettings.ShootTimer));
    }
}
