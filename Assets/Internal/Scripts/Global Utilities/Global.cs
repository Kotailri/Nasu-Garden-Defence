using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static PlayerControls      playerControls;
    public static KeystoneItemManager keystoneItemManager;
    public static ItemUI              itemUI;
    public static ItemPassiveManager  itemPassiveManager;
    public static GardenHealth        gardenHealth;

    // ===== Game State ===== //
    public static bool gameplayStarted = false;
    public static bool isGameOver = false;

    // ===== Player Info ===== //
    public static int RemainingRerolls = 0;
    public static Transform playerTransform;
    public static Vector2 playerMoveVector = Vector2.zero;

    // ===== Debug ===== //
    public static bool DebugMode = false;
    public static bool IsInEditorMode = false;

    // ===== Difficulty ===== //
    public static float EnemySpeedMultiplier = 1f;
    public static float EnemyHealthMultiplier = 1f;
    public static float WaveSpeed = 1f;

    public static float CoinValueMultiplier = 1f;

    public static float ContactSlowAmount = 0.5f;
    public static float ContactSlowTime = 1f;

    // ===== Bounds ===== //
    public static float MaxX = 16.5f;
    public static (float min, float max) XRange = (-14.67f, 17.31f);
    public static (float min, float max) YRange = (-7.5f, 7f);

    // ===== Accessibility ===== // 
    public static float DamageFlashTimer = 0.25f;
    public static float DamageFlashAlpha = 0.25f;
}

public static class GlobalAudio
{
    public static float MusicVolume = 0.5f;
    public static float SoundVolume = 0.5f;
}

public static class ControllerConfiguration
{
    public static double ControllerDeadZone = 0.5f;
    public static float CursorSpeed = 12f;
}