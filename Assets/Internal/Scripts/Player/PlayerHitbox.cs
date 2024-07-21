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
            int dmg = dm.GetDamage();
            //print("Took " + dmg + " from " + collisionObject.name);

            if (dmg > 0) 
            {
                health.SetHealth(-dmg, true);
                GetComponent<PlayerMovement>().ApplySlow(GlobalPlayer.ContactSlowAmount, GlobalPlayer.ContactSlowTime);
                EventManager.TriggerEvent(EventStrings.PLAYER_TAKE_DAMAGE, null);
            }

            if (dm.DestroysSelfOnHit)
            {
                Destroy(dm.gameObject);
            }

            if (dmg > 0)
                StartCoroutine(IFrames());
        }
    }
}
