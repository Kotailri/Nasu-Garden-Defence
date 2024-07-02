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

    public void CreateItems()
    {
        float animTime = 1f;
        float startingHeight = 20f;

        GameObject item1 = Instantiate(SelectObject, ItemLocation1.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
        LeanTween.moveY(item1, ItemLocation1.position.y, animTime).setEaseInOutBounce();

        GameObject item2 = Instantiate(SelectObject, ItemLocation2.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
        LeanTween.moveY(item2, ItemLocation2.position.y, animTime).setEaseInOutBounce();

        GameObject item3 = Instantiate(SelectObject, ItemLocation3.position + new Vector3(0, startingHeight, 0), Quaternion.identity);
        LeanTween.moveY(item3, ItemLocation3.position.y, animTime).setEaseInOutBounce();

        currentItems.Add(item1);
        currentItems.Add(item2);
        currentItems.Add(item3);
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
                Global.waveManager.SpawnNextWave();
            }
        }

        
    }
}
