using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject AlertObject;
    public float AlertTime;

    private void Awake()
    {
        Global.alertManager = this;
    }

    public void CreateAlert(Vector2 alertPosition, AudioEnum alertSound)
    {
        AudioManager.instance.PlaySound(alertSound);
        GameObject g = Instantiate(AlertObject, alertPosition, Quaternion.identity);
        Destroy(g, AlertTime);
    }
}
