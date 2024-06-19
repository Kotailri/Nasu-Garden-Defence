using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static Transform playerTransform;
    public static Transform cursorTransform;

    public static PlayerControls playerControls;
    public static GameOverManager gameOverManager;

    public static void GameOver()
    {
        if (gameOverManager == null) return;
        gameOverManager.DoGameOver();
    }
}

public static class Config
{
    public static double ControllerDeadZone = 0.5f;
    public static float CursorSpeed = 12f;
    public static float ProjectileSpawnDistFromPlayer = 0.75f;
}

public static class AdjustableStats
{
    public static float PlayerMovespeed = 0f;
    public static float BonusProjectileSpeed = 0f;
    public static float InvincibilityDuration = 0.0f;
}

public static class Player
{
    public static GameObject playerGameObject;

    public static void TogglePlayerMoveInput(bool IsOn)
    {
        playerGameObject.GetComponent<PlayerMovement>().ToggleMoveInput(IsOn);
    }
}
