using UnityEngine;

[CreateAssetMenu()]
public class PlayerStatScriptable : ScriptableObject
{
    public string StatName;
    public PlayerStatEnum StatEnum;

    [Space(5f)]
    [TextArea(5, 10)]
    public string StatDescription;

    [Space(5f)]
    public float StatBase;
    public float StatGrowth;
    public PlayerStatGrowthType StatGrowthType;
}