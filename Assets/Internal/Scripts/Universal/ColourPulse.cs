using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPulse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Tooltip("Color to pulse to")]
    [ColorUsage(true, true)]
    public Color pulseColor = Color.red;
    [Tooltip("Duration of the pulse")]
    public float pulseDuration = 2.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!");
            return;
        }
        originalColor = spriteRenderer.color;
        StartPulsing();
    }

    private void StartPulsing()
    {
        LeanTween.value(gameObject, UpdateColor, originalColor, pulseColor, pulseDuration)
                 .setEase(LeanTweenType.easeInOutSine)
                 .setLoopPingPong();
    }

    private void UpdateColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}
