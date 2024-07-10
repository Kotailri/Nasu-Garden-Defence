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

        yield return new WaitForSeconds(GlobalPlayer.GetStatValue(PlayerStatEnum.invincDuration) + Global.keystoneItemManager.ImmortalHarmonyShieldTime);

        if (spriteRenderer)
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        canTakeDamage = true;
    }

    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (canTakeDamage && collisionObject.TryGetComponent(out DamagesPlayerOnHit dm))
        {
            if (Random.Range(0f,1f) > GlobalPlayer.GetStatValue(PlayerStatEnum.dodge))
            {
                health.SetHealth(-dm.GetDamage(), true);
                GetComponent<PlayerMovement>().ApplySlow(GlobalPlayer.ContactSlowAmount, GlobalPlayer.ContactSlowTime);
                EventManager.TriggerEvent(EventStrings.PLAYER_TAKE_DAMAGE, null);
            }
            else
            {
                Global.damageTextSpawner.SpawnText(transform.position, "dodged!", DamageTextType.Status, 1f);
            }
            
            StartCoroutine(IFrames());
        }
    }
}
