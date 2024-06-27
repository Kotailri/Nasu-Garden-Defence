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

        yield return new WaitForSeconds(GlobalPlayer.InvincibilityDuration);

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        canTakeDamage = true;
    }

    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (canTakeDamage && collisionObject.TryGetComponent(out DamagesPlayerOnHit dm))
        {
            health.RemoveHealth(dm.GetDamage());
            Global.damageTextSpawner.SpawnText(transform.position, "-" + dm.GetDamage().ToString(), DamageTextType.Red);
            GetComponent<PlayerMovement>().ApplySlow(GlobalPlayer.ContactSlowAmount, GlobalPlayer.ContactSlowTime);
            StartCoroutine(IFrames());
        }
    }
}
