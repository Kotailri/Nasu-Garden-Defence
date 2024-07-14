using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class StatsUIDisplay
{
    public TextMeshProUGUI StatName;
    public TextMeshProUGUI StatLevel;
}

public class StatsMenuUI : MonoBehaviour
{
    public GameObject StatsObject;
    public Transform StatsAnchorPoint;
    public Button StatsButton;

    private float spacing = 20f;

    [Space(10f)]
    [Tooltip("DO NOT MODIFY")]
    public List<GameObject> statsDisplayList = new();

    private Vector2 activePosition;
    private float leftSlideAmount = 1000f;
    private bool isOpen = false;

    private void Awake()
    {
        leftSlideAmount = GetComponent<RectTransform>().rect.width;

        activePosition = transform.position;
        transform.position = activePosition - new Vector2(leftSlideAmount, 0);
    }

    private void Start()
    {
        int index = 0;
        foreach (var item in GlobalPlayer.PlayerStatDict)
        {
            GameObject g = Instantiate(StatsObject, Vector2.zero, Quaternion.identity);
            g.transform.SetParent(transform);
            g.transform.localPosition = StatsAnchorPoint.localPosition + new Vector3(0,-index * spacing,0);
            statsDisplayList.Add(g);
            index++;
        }

        UpdateStatsDisplay();
        GetComponent<CanvasGroup>().alpha = 0;
    }

    private void UpdateStat(int index, PlayerStat stat)
    {
        StatMenuUIStatObject statObj = statsDisplayList[index].GetComponent<StatMenuUIStatObject>();
        statObj.SetStatNameBox(stat.GetStatName());
        statObj.SetStatlevelBox(stat.GetLevel());
        statObj.SetDescription(stat.GetStatDescription());
    }

    private void UpdateStatsDisplay()
    {
        int index = 0;
        foreach (var item in GlobalPlayer.PlayerStatDict)
        {
            UpdateStat(index, item.Value);
            index++;
        }
    }

    private float slideTime = 0.2f;

    public void OnStatsDisplayOpen()
    {
        if (LeanTween.isTweening(gameObject))
        {
            return;
        }

        if (!isOpen)
        { 
            UpdateStatsDisplay();
            GetComponent<CanvasGroup>().alpha = 1;
            LeanTween.moveX(gameObject, activePosition.x, slideTime).setEaseOutQuad();
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.moveX(gameObject, activePosition.x - leftSlideAmount, slideTime).setEaseOutQuad();
        }

        StatsButton.interactable = false;
        StartCoroutine(WaitButtonTime());

        isOpen = !isOpen;
    }

    private IEnumerator WaitButtonTime()
    {
        yield return new WaitForSeconds(slideTime/2);
        StatsButton.interactable = true;
    }

    public void OnStatsDisplayButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnStatsDisplayOpen();
        }
    }
}
