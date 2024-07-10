using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIToggleEditor : MonoBehaviour
{
    public List<GameObject> UIObjects = new();

    private void Awake()
    {
        foreach (GameObject g in UIObjects)
        {
            g.SetActive(true);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Enable UI")]
    public void EnableUI()
    {
        foreach (GameObject g in UIObjects)
        {
            g.SetActive(true);
        }
    }

    [ContextMenu("Disable UI")]
    public void DisableUI()
    {
        foreach (GameObject g in UIObjects)
        {
            g.SetActive(false);
        }
    }
#endif
}
