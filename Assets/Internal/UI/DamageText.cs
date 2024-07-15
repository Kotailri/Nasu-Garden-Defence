using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DamageTextType
{
    Red,
    White,
    Status,
    Green,
    Crit
}

[Serializable]
public class ColouredDamageText
{
    public DamageTextType type;
    public TextMeshProUGUI textbox;
}

public class DamageText : MonoBehaviour
{
    public float Timer;
    public List<ColouredDamageText> damageTexts = new();

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(TweenAnim());
    }

    public void CreateText(string damageNumber, DamageTextType col)
    {
        foreach (ColouredDamageText damageText in damageTexts)
        {
            if (damageText.type == col)
            {
                damageText.textbox.gameObject.SetActive(true);
                damageText.textbox.text = damageNumber;
            }
            else
            {
                damageText.textbox.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator TweenAnim()
    {
        LeanTween.scale(gameObject, baseScale, 0.1f);
        yield return new WaitForSeconds(0.25f + Timer);
        LeanTween.scale(gameObject, Vector2.zero, 0.15f).setEaseInBounce().setOnComplete(() => { Destroy(gameObject); });
    }
}
