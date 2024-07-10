using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSlugSpit : MonoBehaviour
{
    public GameObject targetIndicator;
    public GameObject fallingSpit;
    public GameObject spitCircle;

    [Space(5f)]
    public float spitDuration;

    [Space(5f)]
    public float targetFadeTime;
    public float spitDelayBeforeFall;
    public float spitFallTime;
    public float spitGrowTime;

    private Vector3 defaultScale = Vector3.one;

    private void Awake()
    {
        spitCircle.GetComponent<Collider2D>().enabled = false;
        defaultScale = spitCircle.transform.localScale;
        spitCircle.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        fallingSpit.transform.localPosition += new Vector3(0, 50, 0);
        StartCoroutine(WaitBeforeSpitfall());
    }

    private IEnumerator WaitBeforeSpitfall()
    {
        yield return new WaitForSeconds(spitDelayBeforeFall);

        LeanTween.alpha(targetIndicator, 0f, targetFadeTime);
        LeanTween.moveLocalY(fallingSpit, 0, spitFallTime)
        .setOnComplete(() => {
            Destroy(fallingSpit);
            spitCircle.GetComponent<Collider2D>().enabled = true;
            LeanTween.scale(spitCircle.gameObject, defaultScale, spitGrowTime);
        });

        Destroy(gameObject, spitDuration);
    }
}
