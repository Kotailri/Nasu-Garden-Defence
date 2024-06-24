using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public void AddItemToUI(ItemScriptable item, List<KeyValuePair<string, string>> descriptionReplacements = null)
    {
        GameObject newItem = Instantiate(UIItemPrefab.gameObject);
        newItem.transform.SetParent(this.transform, false);
        newItem.transform.position += new Vector3(LeftMargin,0,0);
        newItem.GetComponent<UIItem>().SetItem(item, descriptionReplacements);
        ItemInventory.Add(newItem);

        LeftMargin += 80f;

        if (CurrentPage == (ItemInventory.Count / NumSlots) - 1 && ItemInventory.Count % 10 == 1)
        {
            RightArrow.SetActive(true);
        }
    }

    private void ArrowOnClick(int moveDirection)
    {
        if (!ButtonsInteractable)
        {
            return;
        }

        foreach (GameObject item in ItemInventory)
        {
            item.transform.position += new Vector3(800f * moveDirection, 0, 0);
        }

        LeftMargin += (800f * moveDirection);

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
