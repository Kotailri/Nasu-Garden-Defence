using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IItemSelectMng : IManager
{
    public void RerollItems();
    public void SetItemSelected(GameObject item);
    public void ItemSelected();
    public void CreateItems(ItemTypeEnum itemType, bool isReroll = false);
}


public class ItemSelectManager : MonoBehaviour, IItemSelectMng
{
    public GameObject SelectObject;

    [Space(5f)]
    public Transform ItemLocation1;
    public Transform ItemLocation2;
    public Transform ItemLocation3;
    public Transform RerollLocation;

    public GameObject Reroller;

    private GameObject currentReroller;
    private List<GameObject> currentItems = new();
    private List<ItemAdder> currentAdderPool = new();
    private GameObject selectedItem = null;
    private ItemTypeEnum currentItemType = ItemTypeEnum.StatUp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) { RerollItems(); }
    }

    public void RerollItems()
    {
        if (Global.RemainingRerolls > 0)
        {
            Global.RemainingRerolls--;
        }
        else
        {
            return;
        }

        foreach (GameObject item in currentItems)
            currentAdderPool.Remove(item.GetComponent<ItemSelectObject>().GetItemAdder());

        StartCoroutine(ItemRerollCoroutine());

        IEnumerator ItemRerollCoroutine()
        {
            foreach (GameObject item in currentItems)
            {
                currentAdderPool.Remove(item.GetComponent<ItemSelectObject>().GetItemAdder());
                LeanTween.moveY(item, item.transform.position.y + 20f, 1f).setOnComplete(() => { Destroy(item); });
;               yield return new WaitForSeconds(0.25f);
            }
            currentItems.Clear();
            CreateItems(currentItemType, isReroll:true);
        }
        
    }

    float animTime = 1f;
    float startingHeight = 20f;

    public void CreateItems(ItemTypeEnum itemType, bool isReroll=false)
    {
        if (Global.isGameOver) { return; }

        if (!isReroll)
        {
            currentAdderPool = Managers.Instance.Resolve<IItemInventoryMng>().GetFullPool(itemType, Managers.Instance.Resolve<IWaveMng>().GetCurrentWaveIndex());
        }

        currentItemType = itemType;

        StartCoroutine(ItemSpawnCoroutine());

        IEnumerator ItemSpawnCoroutine()
        {
            List<ItemAdder> selectPool = Global.GetRandomElements(currentAdderPool, 3);

            if (!isReroll && currentAdderPool.Count >= 6)
            {
                yield return new WaitForSeconds(1);
                currentReroller = Instantiate(Reroller, RerollLocation.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
                LeanTween.moveY(currentReroller, RerollLocation.position.y, animTime).setEaseInOutBounce();
            }

            if (isReroll && currentAdderPool.Count <= 3) { Destroy(currentReroller); }

            yield return new WaitForSeconds(0.25f);
            

            if (selectPool.Count == 0)
            {
                Managers.Instance.Resolve<IWaveMng>().SpawnNextWave();
                yield break;
            }

            GameObject item1 = Instantiate(SelectObject, ItemLocation1.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item1, ItemLocation1.position.y, animTime).setEaseInOutBounce();
            item1.GetComponent<ItemSelectObject>().SetItemAdder(selectPool[0]);
            currentItems.Add(item1);

            if (selectPool.Count == 1)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.25f);

            GameObject item2 = Instantiate(SelectObject, ItemLocation2.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item2, ItemLocation2.position.y, animTime).setEaseInOutBounce();
            item2.GetComponent<ItemSelectObject>().SetItemAdder(selectPool[1]);
            currentItems.Add(item2);

            if (selectPool.Count == 2)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.25f);

            GameObject item3 = Instantiate(SelectObject, ItemLocation3.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item3, ItemLocation3.position.y, animTime).setEaseInOutBounce();
            item3.GetComponent<ItemSelectObject>().SetItemAdder(selectPool[2]);
            currentItems.Add(item3);
        }
    }

    public void SetItemSelected(GameObject item)
    {
        selectedItem = item;
    }

    public void OnItemSelected(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ItemSelected();
        }
    }

    public void ItemSelected()
    {
        if (!Global.IsWaveOngoing())
        {
            if (selectedItem != null && selectedItem.GetComponent<ItemSelectObject>().isItemActive)
            {
                selectedItem.GetComponent<ItemSelectObject>().AcquireItem();
                foreach (GameObject item in currentItems)
                {
                    Destroy(item);
                }
                Destroy(currentReroller);
                currentItems.Clear();
                currentAdderPool.Clear();
                StartCoroutine(SpawnNextWaveDelay());
            }
        }
    }

    private IEnumerator SpawnNextWaveDelay()
    {
        yield return new WaitForSeconds(1f);
        Managers.Instance.Resolve<IWaveMng>().SpawnNextWave();
    }
}
