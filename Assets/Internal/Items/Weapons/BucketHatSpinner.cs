using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketHatSpinner : PlayerAttack
{
    [Header("Bucket Hat Spinner")]
    public int NumberOfHats;

    [Space(10f)]
    public float SpinDuration;
    public float SpinSpeed;
    public float SpinRadius;

    private List<GameObject> BucketHatList = new();
    private bool isSpinning = false;

    public void Start()
    {
        // Calculate the angle between each object
        float angleStep = 360f / NumberOfHats;

        for (int i = 0; i < NumberOfHats; i++)
        {
            // Calculate the angle for this object
            float angle = i * angleStep;

            // Convert the angle to radians
            float angleInRadians = angle * Mathf.Deg2Rad;

            // Calculate the position
            float x = transform.parent.position.x + Mathf.Cos(angleInRadians) * SpinRadius;
            float y = transform.parent.position.y + Mathf.Sin(angleInRadians) * SpinRadius;

            // Create the object at the calculated position
            Vector3 objectPosition = new Vector3(x, y, transform.parent.position.z);
            GameObject g = Instantiate(AttackPrefab, objectPosition, Quaternion.identity);

            g.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
            g.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
            g.GetComponent<PlayerAttackPrefab>().SetKnockbackTime(KnockbackTime);

            g.transform.SetParent(transform, true);

            BucketHatList.Add(g);
        }

        foreach (GameObject hat in BucketHatList)
        {
            hat.transform.localRotation = Quaternion.identity;
            hat.SetActive(false);
        }
    }

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        isSpinning = true;
        foreach (GameObject hat in BucketHatList)
        {
            hat.SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(SpinHats());
    }

    private void Update()
    {
        if (isSpinning)
        {
            transform.localPosition = Vector3.zero;
            transform.Rotate(new Vector3(0, 0, SpinSpeed * Time.deltaTime));
            foreach (GameObject hat in BucketHatList)
            {
                hat.transform.Rotate(new Vector3(0, 0, -SpinSpeed * Time.deltaTime), Space.Self);
            }
        }
    }

    private IEnumerator SpinHats()
    {
        foreach (GameObject hat in BucketHatList)
        {
            hat.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(SpinDuration);

        foreach (GameObject hat in BucketHatList)
        {
            hat.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        isSpinning = false;
    }
}
