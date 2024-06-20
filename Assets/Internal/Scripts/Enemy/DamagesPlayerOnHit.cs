using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesPlayerOnHit : MonoBehaviour
{
    public int Damage;

    public void SetDamage(int _damage)
    {
        Damage = _damage;   
    }

    public int GetDamage()
    {
        return Damage;
    }
}
