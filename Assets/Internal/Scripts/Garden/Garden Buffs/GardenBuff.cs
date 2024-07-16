using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GardenBuff : MonoBehaviour
{
    public int CurrentLevel = 0;
    public List<int> PriceAtEachLevel = new();

    [Space(10f)]
    public GameObject Info;
    public SpriteRenderer GemGraphic;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;

    public int MaxLevel;
    public bool IsSelected = false;
    protected string DefaultBoxText;

    private void Start()
    {
        MaxLevel = PriceAtEachLevel.Count-1;
        SetStartingLevel();

        //priceText.text = PriceAtEachLevel[CurrentLevel].ToString();
        //levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString();

        Info.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsSelected = true;
            GemGraphic.color = Color.blue;
            Info.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsSelected = false;
            GemGraphic.color = Color.white;
            Info.SetActive(false);
        }
    }

    public virtual bool CanLevelUp()
    {
        if (Global.IsInEditorMode) { return true; }

        if (CurrentLevel == MaxLevel) 
            return false;
        return Global.gardenBuffManager.GetCoins() >= PriceAtEachLevel[CurrentLevel + 1];

    }

    public virtual void Refund()
    {
        int total = 0;
        for (int i = 0; i <= CurrentLevel; i++)
        {
            total += PriceAtEachLevel[i];
        }
        Global.gardenBuffManager.AddCoins(total);
        CurrentLevel = 0;
        CheckLevel();
        UpdateLevel();
    }

    public void BaseLevelUp()
    {
        if (CanLevelUp())
        {
            CurrentLevel++;

            LevelUp();
            UpdateLevel();
            if (!Global.IsInEditorMode) 
            {
                Global.gardenBuffManager.RemoveCoins(PriceAtEachLevel[CurrentLevel]);
            }
            else
            {
                Global.gardenBuffManager.saver.SaveBuffs();
            }
            AudioManager.instance.PlaySound(AudioEnum.LevelUp);
        }
        else
        {
            AudioManager.instance.PlaySound(AudioEnum.Error);
        }

    }

    public abstract void LevelUp();

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            CurrentLevel++;
            LevelUp();
        }

        UpdateLevel();
    }

    public abstract void SetStartingLevel();

    public virtual void UpdateLevel()
    {
        levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString();
        CheckLevel();
    }

    protected void CheckLevel()
    {
        if (CurrentLevel >= MaxLevel)
        {
            priceText.text = "Maxed!";
        }
        else
        {
            priceText.text = PriceAtEachLevel[CurrentLevel+1].ToString();
        }
    }
}
