using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image itemImage;
    public Image box;

    private void Awake()
    {
        box.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        box.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        box.gameObject.SetActive(false);
    }

    public void SetItem(ItemScriptable item, List<KeyValuePair<string, string>> replacements)
    {
        nameText.text = item.ItemName;

        string desc = item.ItemDescription;

        if (replacements != null)
        {
            foreach (var kvp in replacements)
            {
                desc = desc.Replace(kvp.Key, kvp.Value);
            }
        }
        

        descriptionText.text = desc;

        itemImage.sprite = item.ItemIconImage;
        itemImage.color = item.ItemSpriteColor;
    }

    /*
     * new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("fox", "cat"),
            new KeyValuePair<string, string>("dog", "hamster"),
            new KeyValuePair<string, string>("clever", "smart")
        };
     * */
}
