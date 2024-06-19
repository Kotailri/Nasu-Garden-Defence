using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DamageTextType
{
    Red,
    White,
    Status
}

public class DamageText : MonoBehaviour
{
    public float Timer;

    [Space(5f)]
    public TextMeshProUGUI RedText;
    public TextMeshProUGUI WhiteText;
    public TextMeshProUGUI StatusText;


    public void CreateText(string damageNumber, DamageTextType col)
    {
        WhiteText.gameObject.SetActive(false);
        RedText.gameObject.SetActive(false);
        StatusText.gameObject.SetActive(false);

        switch (col)
        {
            case DamageTextType.Red:
                RedText.gameObject.SetActive(true);
                RedText.text = damageNumber;
                break;

            case DamageTextType.White:
                WhiteText.gameObject.SetActive(true);
                WhiteText.text = damageNumber;
                break;

            case DamageTextType.Status:
                StatusText.gameObject.SetActive(true);
                StatusText.text = damageNumber;
                break;
        }
    }

    private void Start()
    {
        Destroy(gameObject, Timer);
    }
}
