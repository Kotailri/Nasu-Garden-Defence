using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenHealthUI : MonoBehaviour
{
    public GameObject HealthImage;

    private void Awake()
    {
        Global.gardenHealthUI = this;
    }

    public void UpdateUI(int hp)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float padding = 40f;
        for (int i = 0; i < hp; i++) 
        {
            GameObject g = Instantiate(HealthImage, Vector3.zero, Quaternion.identity);
            g.transform.position += new Vector3(padding * i, 0, 0);
            g.transform.SetParent(transform, false);
            
        }
    }

}
