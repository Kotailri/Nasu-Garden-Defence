using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightControl : MonoBehaviour
{
    public Light2D GlobalLight;
    public Light2D PlayerSpotlight;
    public GameObject TransitionUI;
    public TextMeshProUGUI TransitionUIText;

    [Header("Mountain BG")]
    public SpriteRenderer MountainBackground;
    public Sprite MountainDay;
    public Sprite MountainNight;

    [ContextMenu("Toggle Night")]
    public void ToggleNight()
    {
        TransitionUIText.text = "The Sun Sets";
        TweenTransitionUI(false);
    }

    [ContextMenu("Toggle Day")]
    public void ToggleDay()
    {
        TransitionUIText.text = "The Sun Rises";
        TweenTransitionUI(true);
    }

    private float moveTime = 1.0f;
    private void TweenTransitionUI(bool isDay)
    {
        LeanTween.moveLocalY(TransitionUI, 0, moveTime).setOnComplete(()=>
        {
            if (isDay)
            {
                MountainBackground.sprite = MountainDay;
                MountainBackground.color = Color.white;
                PlayerSpotlight.gameObject.SetActive(false);
            }
            else
            {
                MountainBackground.sprite = MountainNight;
                PlayerSpotlight.gameObject.SetActive(true);
                MountainBackground.color = new Color(0.7264151f,0.7264151f,0.7264151f);
            }

            StartCoroutine(MoveUIBack());
        });

        IEnumerator MoveUIBack()
        {
            yield return new WaitForSeconds(moveTime);
            LeanTween.moveLocalY(TransitionUI, -720, moveTime).setOnComplete(()=>
            {
                TransitionUI.transform.localPosition = new Vector3(0,720,0);
            });
        }
    }
}
