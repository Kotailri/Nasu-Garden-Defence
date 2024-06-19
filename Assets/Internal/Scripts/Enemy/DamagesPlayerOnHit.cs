using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CallsTriggerCollisions))]
public class DamagesPlayerOnHit : MonoBehaviour
{
    public int Damage;
    public int GetDamage()
    {
        return Damage;
    }
}
