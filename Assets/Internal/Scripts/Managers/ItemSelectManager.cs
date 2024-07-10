using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSelectManager : MonoBehaviour
{
    public GameObject SelectObject;

    [Space(5f)]
    public Transform ItemLocation1;
    public Transform ItemLocation2;
    public Transform ItemLocation3;

    private List<GameObject> currentItems = new();
    private GameObject selectedItem = null;

    private void Awake()
    {
        Global.itemSelectManager = this;
    }

    public void CreateItems(ItemTier tier)
    {
        if (Global.isGameOver) { return; }

        float animTime = 1f;
        float startingHeight = 20f;

        StartCoroutine(ItemSpawnCoroutine());

        IEnumerator ItemSpawnCoroutine()
        {
            yield return new WaitForSeconds(1f);

            List<ItemAdder> randomItems = Global.itemInventoryManager.GetRandomFromPool(3, tier, Global.waveManager.CurrentWaveIndex);

            if (randomItems.Count == 0)
            {
                Global.waveManager.SpawnNextWave();
                yield break;
            }

            GameObject item1 = Instantiate(SelectObject, ItemLocation1.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item1, ItemLocation1.position.y, animTime).setEaseInOutBounce();
            item1.GetComponent<ItemSelectObject>().SetItem(randomItems[0]);
            currentItems.Add(item1);

            if (randomItems.Count == 1)
                yield break;

            yield return new WaitForSeconds(0.25f);

            GameObject item2 = Instantiate(SelectObject, ItemLocation2.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item2, ItemLocation2.position.y, animTime).setEaseInOutBounce();
            item2.GetComponent<ItemSelectObject>().SetItem(randomItems[1]);
            currentItems.Add(item2);

            if (randomItems.Count == 2) 
                yield break;

            yield return new WaitForSeconds(0.25f);

            GameObject item3 = Instantiate(SelectObject, ItemLocation3.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
            LeanTween.moveY(item3, ItemLocation3.position.y, animTime).setEaseInOutBounce();
            item3.GetComponent<ItemSelectObject>().SetItem(randomItems[2]);
            currentItems.Add(item3);
        }
    }

    public void SetItemSelected(GameObject item)
    {
        selectedItem = item;
    }

    public void OnItemSelected(InputAction.CallbackContext context)
    {
        if (context.performed && !Global.waveManager.IsWaveOngoing())
        {
            if (selectedItem != null && selectedItem.GetComponent<ItemSelectObject>().isItemActive) 
            {
                selectedItem.GetComponent<ItemSelectObject>().AcquireItem();
                foreach (GameObject item in currentItems)
                {
                    Destroy(item);
                }
                StartCoroutine(SpawnNextWaveDelay());
            }
        }
    }

    private IEnumerator SpawnNextWaveDelay()
    {
        yield return new WaitForSeconds(1f);
        Global.waveManager.SpawnNextWave();
    }
}
