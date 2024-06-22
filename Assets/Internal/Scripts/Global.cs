using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static bool gameplayStarted = false;

    public static Transform playerTransform;
    public static Transform cursorTransform;

    public static PlayerControls playerControls;
    public static GameOverManager gameOverManager;
    public static GardenHealthUI gardenHealthUI;
    public static WaveManager waveManager;
    public static TextSpawner damageTextSpawner;
    public static KeystoneItemManager keystoneItemManager;

    public static float MaxX = 18.47f;

    public static (float min, float max) XRange = (-14.13f, 18.31f);
    public static (float min, float max) YRange = (-7f, 6.95f);

    public static void GameOver()
    {
        if (gameOverManager == null) return;
        gameOverManager.DoGameOver();
    }

    public static List<GameObject> GetActiveEnemies()
    {
        List<GameObject> activeEnemies = new();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (MathHelper.IsBetweenFloatsInclusive(enemy.transform.position.x, XRange.min, XRange.max)
             && MathHelper.IsBetweenFloatsInclusive(enemy.transform.position.y, YRange.min, YRange.max))
            {
                activeEnemies.Add(enemy);
            }
        }
        return activeEnemies;
    }


}

public static class MathHelper
{
    public static bool IsBetweenFloatsInclusive(float toCheck, float min, float max)
    {
        return (toCheck >= min) && (toCheck <= max);
    }
}

public static class Config
{
    public static double ControllerDeadZone = 0.5f;
    public static float CursorSpeed = 12f;
}

public static class PlayerScriptableSettings
{
    //[Header("Moving")]
    public static float PlayerMovespeed;

    //[Header("Shooting")]
    public static float ShootTimer;
    public static int ProjectileDamage;
    public static float ProjectileSpeed;

    //[Space(5f)]
    public static GameObject ProjectilePrefab;

    //[Header("Combat")]
    public static float InvincibilityDuration;
    public static float PlayerDamageAmp;

    public static float ContactSlowAmount;
    public static float ContactSlowTime;

    // Items

}

public static class Player
{
    public static GameObject playerGameObject;

    public static void TogglePlayerMoveInput(bool IsOn)
    {
        playerGameObject.GetComponent<PlayerMovement>().ToggleMoveInput(IsOn);
    }
}
