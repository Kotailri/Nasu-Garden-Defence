using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveDebugMenu : MonoBehaviour, IDebugMenu
{
    public Button SetWaveButton;
    public Button EndWaveButton;
    public TMP_Dropdown WaveDropdown;

    private IWaveMng waveManager;
    private DebugMenuController debugMenuController;

    private Dictionary<int, WaveWithReward> wavesDict = new();
    private int selectedWaveIndex = -1;

    private void Start()
    {
        waveManager = Managers.Instance.Resolve<IWaveMng>();

        WaveDropdown.options.Clear();

        int index = 0;
        foreach (var option in waveManager.GetWaves())
        {
            wavesDict.Add(index, option);
            WaveDropdown.options.Add(new TMP_Dropdown.OptionData("[" + index.ToString() + "] " + option.Wave.name));
            index++;
        }

        SetWaveButton.onClick.AddListener(() => 
        {
            debugMenuController.ToggleDebugMenu(false);
            waveManager.StartWave(selectedWaveIndex);
        });

        EndWaveButton.onClick.AddListener(() =>
        {
            debugMenuController.ToggleDebugMenu(false);
            waveManager.KillWave();
        });

        WaveDropdown.onValueChanged.AddListener((int val) =>
        {
            selectedWaveIndex = val;
            SetWaveButton.interactable = true;
        });
    }

    public void LoadMenu()
    {
        SetWaveButton.interactable = false;
        selectedWaveIndex = -1;
        WaveDropdown.captionText.text = "[" + waveManager.GetCurrentWaveIndex().ToString() + "] " + waveManager.GetCurrentWave().Wave.name;
    }

    public void SetDebugMenuController(DebugMenuController controller)
    {
        debugMenuController = controller;
    }
    
}
