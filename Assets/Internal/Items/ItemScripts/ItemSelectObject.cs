using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectObject : MonoBehaviour
{
    public Image ItemImage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;

    [Space(5f)]
    public TextMeshProUGUI AcquireItemBox;

    [Space(5f)]
    public GameObject Canvas;

    private Vector3 defaultScale;
    private ItemAdder itemAdder;
    public bool isItemActive = false;

    private void Start()
    {
        defaultScale = transform.localScale;
        Canvas.transform.localScale = Vector3.zero;
        LeanTween.scale(Canvas, defaultScale, 0.5f);
        AcquireItemBox.text = "";

        Canvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SetItem(ItemAdder _itemAdder)
    {
        itemAdder = _itemAdder;
        ItemImage.sprite = itemAdder.GetInfo().ItemIconImage;
        ItemName.text = itemAdder.GetInfo().ItemName;
        ItemDescription.text = itemAdder.GetInfo().ItemDescription;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Canvas.transform.localScale = defaultScale + new Vector3(0.1f,0.1f,0.1f);
            Global.itemSelectManager.SetItemSelected(gameObject);
            isItemActive = true;

            AcquireItemBox.text = "Acquire Item [Space]";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Canvas.transform.localScale = defaultScale;
            isItemActive = false;

            AcquireItemBox.text = "";
        }
    }

    public void ItemSelectedClicked()
    {
        print("clicked");
        Global.itemSelectManager.SetItemSelected(gameObject);
        Global.itemSelectManager.ItemSelected();
    }

    public void AcquireItem()
    {
        isItemActive = false;
        itemAdder.OnItemGet();
        AudioManager.instance.PlaySound(AudioEnum.ThingPlaced);
        Global.itemInventoryManager.PoolToInventory(itemAdder);
    }
}
