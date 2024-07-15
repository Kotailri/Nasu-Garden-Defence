using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderPathing : MonoBehaviour
{
    public Vector2 wanderAreaCenter;
    public Vector2 wanderAreaSize;

    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float maxDistance = 2f;

    private Vector2 targetPosition;
    private float speed;

    public bool directionFlip;
    public bool defaultFaceRight;

    void Start()
    {
        StartCoroutine(Wander());
    }

    private IEnumerator Wander()
    {
        yield return new WaitForSeconds(Random.Range(0, maxWaitTime));

        while (true)
        {
            SetNewRandomTarget();

            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                MoveTowardsTarget();
                yield return null;
            }

            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wanderAreaCenter.x - wanderAreaSize.x / 2, wanderAreaCenter.x + wanderAreaSize.x / 2),
                                         Mathf.Clamp(transform.position.y, wanderAreaCenter.y - wanderAreaSize.y / 2, wanderAreaCenter.y + wanderAreaSize.y / 2));
    }

    private void SetNewRandomTarget()
    {
        Vector3 randomPoint;
        float distance;

        do
        {
            randomPoint = wanderAreaCenter + new Vector2(
                Random.Range(-wanderAreaSize.x / 2, wanderAreaSize.x / 2),
                Random.Range(-wanderAreaSize.y / 2, wanderAreaSize.y / 2)
            );
            distance = Vector2.Distance(transform.position, randomPoint);
        } while (distance > maxDistance);

        targetPosition = randomPoint;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - (Vector2)transform.position).normalized;
        
        if (directionFlip)
        {
            if (defaultFaceRight)
            {
                if (direction.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                if (direction.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wanderAreaCenter, wanderAreaSize);
    }
}
