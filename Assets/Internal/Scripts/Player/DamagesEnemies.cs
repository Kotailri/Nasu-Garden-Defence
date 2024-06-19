using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesEnemies : MonoBehaviour
{
    public int Damage;
    public int GetDamage()
    {
        return Mathf.FloorToInt(Damage * PlayerScriptableSettings.PlayerDamageAmp);
    }
}
