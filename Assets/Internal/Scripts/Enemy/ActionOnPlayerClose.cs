using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnPlayerClose : MonoBehaviour
{
    public float radius;

    [Space(5f)]
    public UnityEvent EnterActionEvent; 
    public UnityEvent ExitActionEvent; 

    private bool isEnterAction = false;

    protected virtual void Update()
    {
        if (isEnterAction)
        {
            if (Vector2.Distance(transform.position, Global.playerTransform.position) > radius)
            {
                ExitActionEvent?.Invoke();
                isEnterAction = false;
            }
        }

        else

        if (!isEnterAction)
        {
            if (Vector2.Distance(transform.position, Global.playerTransform.position) <= radius)
            {
                EnterActionEvent?.Invoke();
                isEnterAction = true;
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
