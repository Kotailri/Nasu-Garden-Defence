using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IHasTriggerStay
{
    private PlayerHealth health;
    private bool canTakeDamage = true;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
    }

    private IEnumerator IFrames()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        canTakeDamage = false;

        yield return new WaitForSeconds(AdjustableStats.InvincibilityDuration);

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        canTakeDamage = true;
    }

    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (canTakeDamage && collisionObject.TryGetComponent(out DamagesPlayerOnHit dm))
        {
            health.RemoveHealth(dm.GetDamage());
            StartCoroutine(IFrames());
        }
    }
}
