using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public SpriteRenderer FlashSprite;

    private void Awake()
    {
        FlashSprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        gameObject.AddComponent<SyncSpriteMask>();
        FlashSprite.color = new Color(1,1,1,0.25f);
        FlashSprite.gameObject.SetActive(false);
    }

    public void DoDamageFlash()
    {
        FlashSprite.gameObject.SetActive(true);
        StartCoroutine(FlashTimer());
        IEnumerator FlashTimer()
        {
            yield return new WaitForSeconds(Global.DamageFlashTimer);
            FlashSprite.gameObject.SetActive(false);
        }
    }
}
