using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    public GameObject AttackPrefab;
    public bool IsFacingRequired = false;

    public void SetAttackPrefab(GameObject _prefab)
    {
        AttackPrefab = _prefab;
    }

    public void SetRequiredPetFacing(bool _facingRequired)
    {
        IsFacingRequired = _facingRequired;
    }

    public abstract void DoAttack(Vector2 attackPosition, int attackCount);
}
