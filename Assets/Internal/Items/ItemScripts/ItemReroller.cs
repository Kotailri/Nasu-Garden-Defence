using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemReroller : MonoBehaviour
{
    public GameObject IconGraphic;
    public GameObject AcquireText;
    public TextMeshProUGUI RerollsText;

    private bool canReroll = true;

    private void Start()
    {
        UpdateRerollText();
        AcquireText.SetActive(false);
    }

    private void UpdateRerollText()
    {
        RerollsText.text = "Rerolls: " + Global.RemainingRerolls.ToString();
    }

    public void Hover()
    {
        IconGraphic.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
        AcquireText.SetActive(true);
    }

    public void Unhover()
    {
        IconGraphic.GetComponent<SpriteRenderer>().color = Color.white;
        AcquireText.SetActive(false);
    }

    private IEnumerator WaitRerollCooldown()
    {
        GetComponent<Collider2D>().enabled = false;
        IconGraphic.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(4);
        canReroll = true;
        IconGraphic.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Collider2D>().enabled = true;
    }

    public void RerollInteract()
    {
        if (canReroll && Global.RemainingRerolls > 0)
        {
            canReroll = false;
            StartCoroutine(WaitRerollCooldown());
            Global.itemSelectManager.RerollItems();
            UpdateRerollText();
        }
        else
        {
            AudioManager.instance.PlaySound(AudioEnum.Error);
        }
        
    }
}
