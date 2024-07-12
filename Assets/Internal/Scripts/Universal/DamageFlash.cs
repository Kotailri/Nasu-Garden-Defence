using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public SpriteRenderer FlashSprite;
    private SyncSpriteMask ssm;

    private void Awake()
    {
        FlashSprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        ssm = gameObject.AddComponent<SyncSpriteMask>();
        ssm.ToggleMask(false);

        FlashSprite.color = new Color(1,1,1,Global.DamageFlashAlpha);
        FlashSprite.gameObject.SetActive(false);
    }

    public void DoDamageFlash()
    {
        FlashSprite.gameObject.SetActive(true);
        ssm.ToggleMask(true);

        StartCoroutine(FlashTimer());
        IEnumerator FlashTimer()
        {
            yield return new WaitForSeconds(Global.DamageFlashTimer);

            ssm.ToggleMask(false);
            FlashSprite.gameObject.SetActive(false);
        }
    }
}
