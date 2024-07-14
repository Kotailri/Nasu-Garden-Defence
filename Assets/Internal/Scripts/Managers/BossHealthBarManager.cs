using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealthBarManager : MonoBehaviour
{
    public ProgressBar bar;
    public TextMeshProUGUI bossNameBox;

    [Space(5f)]
    public float HealthBarLoadTime;
    public bool IsBarLoaded = false;

    private Vector3 activePosition = Vector3.one;
    private Vector3 inactivePosition = Vector3.one;

    private void Awake()
    {
        Global.bossHealthBarManager = this;
        activePosition = transform.localPosition;
        inactivePosition = transform.localPosition - new Vector3(0,100f,0);

        transform.localPosition = inactivePosition;
    }

    private bool isActive = false;
    public bool IsBossHealthActive()
    {
        return isActive;
    }

    public void SetBossName(string _bossName)
    {
        bossNameBox.text = _bossName;
    }

    public void ActivateHealthBar(float _barLoadTime)
    {
        isActive = true;
        HealthBarLoadTime = _barLoadTime;
        IsBarLoaded = false;
        LeanTween.moveLocal(gameObject, activePosition, 1.5f);
        LoadHealthBar();
    }

    public void UpdateHealthBar(float val)
    {
        if (IsBarLoaded)
        {
            bar.UpdateValue(val);
        }
        else
        {
            bar.UpdateValue(0);
        }
    }

    public void DeactivateHealthBar()
    {
        if (gameObject == null) return;
        IsBarLoaded = false;
        isActive = false;
        
        bar.UpdateValue(0);
        LeanTween.moveLocal(gameObject, inactivePosition, 1.5f);
        
    }

    private void LoadHealthBar()
    {
        if (!isActive) { return; }

        LeanTween.value(gameObject, 0f, 1f, HealthBarLoadTime).setOnUpdate((float val) =>
        {
            bar.UpdateValue(val);
        }).setOnComplete(() => { IsBarLoaded = true; });
    }
}
