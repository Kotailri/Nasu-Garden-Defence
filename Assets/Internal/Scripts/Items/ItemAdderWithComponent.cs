using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemAdderWithComponent : ItemAdder
{
    public MonoScript component;

    public override void OnItemGet()
    {
        AddItemToUI();
        Global.playerTransform.gameObject.AddComponent(component.GetClass());
    }
}
