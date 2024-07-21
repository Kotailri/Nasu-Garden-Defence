using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamagesPlayerOnHit : MonoBehaviour
{
    public UnityEvent OnPlayerContactEvents;

    [Space(10f)]
    public int Damage;
    public bool DestroysSelfOnHit = false;

    public void SetDamage(int _damage)
    {
        Damage = _damage;   
    }

    public int GetDamage()
    {
        OnPlayerContactEvents?.Invoke();
        return Damage;
    }
}
