using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Create Item Scriptable")]
public class ItemScriptable : ScriptableObject
{
    public string ItemName;
    [TextArea(5,15)]
    public string ItemDescription;
    public Sprite ItemIconImage;
    public Color ItemSpriteColor;
}

[CreateAssetMenu(menuName = "Item/Create Item Scriptable with Component")]
public class ItemScriptableComponent : ItemScriptable
{
    public MonoScript ItemScript;
}