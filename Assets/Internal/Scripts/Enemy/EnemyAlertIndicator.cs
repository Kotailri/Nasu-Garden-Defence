using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlertIndicator : MonoBehaviour
{
    public AudioEnum sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garden"))
        {
            Managers.Instance.Resolve<IAlertMng>().CreateAlert(new(17.75f, transform.position.y), sound);
        }
    }
}
