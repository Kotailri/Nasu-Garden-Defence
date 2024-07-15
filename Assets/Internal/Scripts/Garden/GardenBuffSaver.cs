using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBuffSaver : MonoBehaviour
{
    private readonly string defaultPrefix = "KTR_NasGarDef_";
    public string prefix;

    private readonly string CoinsString = "Coins_sav";

    private readonly string CoinDropChanceLevelString = "CoinDropChanceLevelString_sav";
    private readonly string CoinMagnetDistanceLevelString = "CoinMagnetDistanceLevelString_sav";
    private readonly string PlayerRegenerationLevelString = "PlayerRegenerationLevelString_sav";
    private readonly string GardenHealAfterWaveLevelString = "GardenHealAfterWaveLevelString_sav";
    private readonly string LevelToSkipLevelString = "LevelToSkipLevelString_sav";
    private readonly string PlayerPercentHealAfterWaveLevelString = "PlayerPercentHealAfterWaveLevelString_sav";

    public void LoadBuffs()
    {
        //print("loaded garden buffs");

        GlobalGarden.Coins = PlayerPrefs.GetInt(defaultPrefix + prefix + CoinsString, 0);

        GlobalGarden.CoinDropChanceLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + CoinDropChanceLevelString, 0);
        GlobalGarden.CoinMagnetDistanceLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + CoinMagnetDistanceLevelString, 0);
        GlobalGarden.PlayerRegenerationLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + PlayerRegenerationLevelString, 0);
        GlobalGarden.GardenHealAfterWaveLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + GardenHealAfterWaveLevelString, 0);
        GlobalGarden.LevelToSkipLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + LevelToSkipLevelString, 0);
        GlobalGarden.PlayerPercentHealAfterWaveLevel = PlayerPrefs.GetInt(defaultPrefix + prefix + PlayerPercentHealAfterWaveLevelString, 0);
    }

    public void SaveBuffs()
    {
        //print("saved garden buffs");

        PlayerPrefs.SetInt(defaultPrefix + prefix + CoinsString, GlobalGarden.Coins);

        PlayerPrefs.SetInt(defaultPrefix + prefix + CoinDropChanceLevelString, GlobalGarden.CoinDropChanceLevel);
        PlayerPrefs.SetInt(defaultPrefix + prefix + CoinMagnetDistanceLevelString, GlobalGarden.CoinMagnetDistanceLevel);
        PlayerPrefs.SetInt(defaultPrefix + prefix + PlayerRegenerationLevelString, GlobalGarden.PlayerRegenerationLevel);
        PlayerPrefs.SetInt(defaultPrefix + prefix + GardenHealAfterWaveLevelString, GlobalGarden.GardenHealAfterWaveLevel);
        PlayerPrefs.SetInt(defaultPrefix + prefix + LevelToSkipLevelString, GlobalGarden.LevelToSkipLevel);
        PlayerPrefs.SetInt(defaultPrefix + prefix + PlayerPercentHealAfterWaveLevelString, GlobalGarden.PlayerPercentHealAfterWaveLevel);
    }
}
