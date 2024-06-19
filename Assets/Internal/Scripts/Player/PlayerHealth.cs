using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    [Space(10f)]
    public ProgressBar bar;

    private void Start()
    {
        CurrentHP = MaxHP;
        bar.UpdateValue((float)CurrentHP / (float)MaxHP);
    }

    public int GetHealth()
    {
        return CurrentHP;
    }

    public int GetMaxHealth()
    {
        return MaxHP;
    }

    public void AddHealth(int _hp)
    {
        CurrentHP += _hp;
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        bar.UpdateValue((float)CurrentHP/(float)MaxHP);
    }

    public void RemoveHealth(int _hp)
    {
        CurrentHP -= _hp;
        CheckDeath();
        bar.UpdateValue((float)CurrentHP / (float)MaxHP);
    }

    public void AddMaxHealth(int _hp)
    {
        CurrentHP += _hp;
        MaxHP = _hp;
        bar.UpdateValue((float)CurrentHP / (float)MaxHP);
    }

    public void RemoveMaxHealth(int _hp)
    {
        MaxHP -= _hp;
        if (MaxHP <= 0) { MaxHP = 1; }

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        bar.UpdateValue((float)CurrentHP / (float)MaxHP);
    }

    private void CheckDeath()
    {
        if (CurrentHP <= 0)
        {
            Global.GameOver();
        }
    }
}
