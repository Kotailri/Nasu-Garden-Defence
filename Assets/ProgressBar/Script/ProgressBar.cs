using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{
    public bool HideOnFull;

    [Header("Bar Setting")]
    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;

    private Image bar, barBackground;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            //UpdateValue(barValue);

        }
    }    

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
    }

    private void Start()
    {
        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;

        //UpdateValue(barValue);
        CheckVisibility();

    }

    private void CheckVisibility()
    {
        if (!HideOnFull)
        {
            return;
        }

        if (bar.fillAmount == 1)
        {
            bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0);
            barBackground.color = new Color(barBackground.color.r, barBackground.color.g, barBackground.color.b, 0);
        }
        else
        {
            bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1);
            barBackground.color = new Color(barBackground.color.r, barBackground.color.g, barBackground.color.b, 1);
        }
    }

    public void UpdateValue(float percent)
    {
        bar.fillAmount = percent;

        if (Alert/100f >= percent)
        {
            bar.color = BarAlertColor;
        }
        else
        {
            bar.color = BarColor;
        }
        CheckVisibility();
    }


    private void Update()
    {
        if (!Application.isPlaying)
        {           
            UpdateValue(50);

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;           
        }
    }

}
