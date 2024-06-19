using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface that allows objects to have trigger collision events
/// </summary>
public interface IHasTriggerEnter
{
    public void OnTriggerEnterEvent(GameObject collisionObject);
}

public interface IHasTriggerExit
{
    public void OnTriggerExitEvent(GameObject collisionObject);
}

public interface IHasTriggerStay
{
    public void OnTriggerStayEvent(GameObject collisionObject);
}
