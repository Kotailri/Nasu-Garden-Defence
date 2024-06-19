using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform Object;

    [Space(10f)]
    public bool FollowX;
    public bool FollowY;
    public bool FollowZ;

    [Space(10f)]
    public bool Instant;
    public float Speed;

    private void Update()
    {
        float posX = (FollowX ? Object.position.x : transform.position.x);
        float posY = (FollowY ? Object.position.y : transform.position.y);
        float posZ = (FollowZ ? Object.position.z : transform.position.z);

        if (Instant)
        {
            transform.position = new Vector3(posX, posY, posZ);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, posZ), Speed * Time.deltaTime);
        }
    }
}
