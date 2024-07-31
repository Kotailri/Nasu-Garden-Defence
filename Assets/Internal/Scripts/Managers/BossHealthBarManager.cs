using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IBossHealthBarMng : IManager
{
    public bool IsBossHealthActive();
    public bool IsBarLoaded();
    public void SetBossName(string _bossName);
    public void ActivateHealthBar(float _barLoadTime);
    public void UpdateHealthBar(float val);
    public void DeactivateHealthBar();
}

public class BossHealthBarManager : MonoBehaviour, IBossHealthBarMng
{
    public ProgressBar bar;
    public TextMeshProUGUI bossNameBox;

    [Space(5f)]
    public float HealthBarLoadTime;
    private bool _isBarLoaded = false;

    private Vector3 activePosition = Vector3.one;
    private Vector3 inactivePosition = Vector3.one;

    private void Awake()
    {
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
        _isBarLoaded = false;
        LeanTween.moveLocal(gameObject, activePosition, 1.5f);
        LoadHealthBar();
    }

    public void UpdateHealthBar(float val)
    {
        if (_isBarLoaded)
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
        _isBarLoaded = false;
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
        }).setOnComplete(() => { _isBarLoaded = true; });
    }

    public bool IsBarLoaded()
    {
        return _isBarLoaded;
    }
}
