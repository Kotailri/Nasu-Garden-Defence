using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLossUI : MonoBehaviour
{
    public GameObject FocusLossUIObject;

    private void OnEnable()
    {
        Application.focusChanged += OnFocusChanged;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnFocusChanged;
    }

    private void OnFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            FocusLossUIObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            FocusLossUIObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}