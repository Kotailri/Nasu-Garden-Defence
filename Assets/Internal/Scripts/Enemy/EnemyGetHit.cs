using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    public void GetHit(int damage)
    {
        GetComponent<EnemyHealth>().TakeDamage(damage);
    }
}
