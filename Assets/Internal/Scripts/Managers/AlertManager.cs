using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlertMng : IManager
{
    public void CreateAlert(Vector2 alertPosition, AudioEnum alertSound);
}

public class AlertManager : MonoBehaviour, IAlertMng
{
    public GameObject AlertObject;
    public float AlertTime;

    public void CreateAlert(Vector2 alertPosition, AudioEnum alertSound)
    {
        AudioManager.instance.PlaySound(alertSound);
        GameObject g = Instantiate(AlertObject, alertPosition, Quaternion.identity);
        Destroy(g, AlertTime);
    }
}
