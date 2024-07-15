using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public List<GameObject> ItemInventory = new();
    public UIItem UIItemPrefab;

    private int NumSlots = 10;
    private int CurrentPage = 0;
    private float LeftMargin = 0f;
    private bool ButtonsInteractable = true;

    [Space(10f)]
    public GameObject LeftArrow;
    public GameObject RightArrow;

    private void Awake()
    {
        Global.itemUI = this;
    }

    private void Start()
    {
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
    }

    public void ReplaceItemInUI(ItemScriptable _old, ItemScriptable _new, List<KeyValuePair<string, string>> descriptionReplacements = null)
    {
        foreach (GameObject _item in ItemInventory)
        {
            if (_item.TryGetComponent(out UIItem uIItem))
            {
                if (uIItem.itemScriptable.IsEqual(_new))
                {
                    uIItem.SetItem(_new, descriptionReplacements);
                    return;
                }
            }
        }
        print("Could not replace" + _old.ItemName + " with " + _new.ItemName + "in UI");
    }

    public void AddItemToUI(ItemScriptable item, List<KeyValuePair<string, string>> descriptionReplacements = null)
    {
        GameObject newItem = Instantiate(UIItemPrefab.gameObject, Vector2.zero, Quaternion.identity);

        newItem.transform.SetParent(this.transform, false);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.position += new Vector3(LeftMargin,0,0);
        newItem.GetComponent<UIItem>().SetItem(item, descriptionReplacements);
        ItemInventory.Add(newItem);

        LeftMargin += 80f;

        if (CurrentPage == (ItemInventory.Count / NumSlots) - 1 && ItemInventory.Count % NumSlots == 1)
        {
            RightArrow.SetActive(true);
        }
    }

    private void ArrowOnClick(int moveDirection)
    {
        float moveAmount = 80f * NumSlots;

        if (!ButtonsInteractable)
        {
            return;
        }

        foreach (GameObject item in ItemInventory)
        {
            item.transform.position += new Vector3(moveAmount * moveDirection, 0, 0);
        }

        LeftMargin += (moveAmount * moveDirection);

        if (CurrentPage == 0)
        {
            LeftArrow.SetActive(false);
        }
        else
        {
            LeftArrow.SetActive(true);
        }

        if (CurrentPage == (ItemInventory.Count / NumSlots))
        {
            RightArrow.SetActive(false);
        }
        else
        {
            RightArrow.SetActive(true);
        }

        StartCoroutine(ArrowClickCooldown());
    }

    public void RightArrowOnClick()
    {
        CurrentPage++;
        ArrowOnClick(-1);
        
    }

    public void LeftArrowOnClick()
    {
        CurrentPage--;
        ArrowOnClick(1);
        
    }

    private IEnumerator ArrowClickCooldown()
    {
        ButtonsInteractable = false;
        yield return new WaitForSeconds(0.25f);
        ButtonsInteractable = true;
    }

}
