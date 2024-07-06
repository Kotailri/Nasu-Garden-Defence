using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatMenuUIStatObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI StatNameBox;
    public TextMeshProUGUI StatLevelBox;

    [Space(5f)]
    public TextMeshProUGUI StatDescriptionBox;

    public void SetStatNameBox(string _name)
    {
        StatNameBox.text = _name;
    }

    public void SetStatlevelBox(int level)
    {
        StatLevelBox.text = level.ToString();
    }

    public void SetDescription(string _description)
    {
        //StatDescriptionBox.text = _description;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO show hover description
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TODO hide hover description
    }
}
