using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AffectedByAmplifier
{
    public void SaveShootPosition();

    public float GetAmplifiedRangeAmount(Vector2 startPosition, Vector2 endPosition);
}
