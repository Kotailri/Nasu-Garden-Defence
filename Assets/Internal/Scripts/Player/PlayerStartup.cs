using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartup : MonoBehaviour
{
    private void Awake()
    {
        Global.playerTransform = transform;
        Global.RemainingRerolls = GlobalGarden.ItemRerolls;

        #if UNITY_EDITOR
                Global.IsInEditorMode = true;
        #endif
    }
}
