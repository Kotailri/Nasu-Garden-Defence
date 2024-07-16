using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBuffResetter : MonoBehaviour
{
    public GameObject HideObject;
    public SpriteRenderer IconGraphic;

    private void Awake()
    {
        HideObject.SetActive(false);
    }

    public void ToggleShow(bool show)
    {
        HideObject.SetActive(show);
        IconGraphic.color = show ? Color.blue : Color.white;
    }

    public void ResetGarden()
    {
        foreach (GardenBuff buff in Global.gardenBuffManager.gardenBuffList)
        {
            buff.Refund();
        }
        Global.gardenBuffManager.saver.SaveBuffs();
    }
}
