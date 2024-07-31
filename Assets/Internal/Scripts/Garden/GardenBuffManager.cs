using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IGardenBuffMng : IManager
{
    public void AddCoins(int _coins);
    public void RemoveCoins(int _coins);
    public int GetCoins();

    public void SaveBuffs();
    public void LoadBuffs();

    public List<GardenBuff> GetGardenBuffList();
}

public class GardenBuffManager : MonoBehaviour, IGardenBuffMng
{
    public GameObject ClefCoin;
    public TextMeshProUGUI coinText;

    [Space(5f)]
    public GardenBuffSaver saver;

    [Header("Buffs")]
    public List<GardenBuff> gardenBuffList = new();

    private void Awake()
    {
        saver.LoadBuffs();
    }

    public List<GardenBuff> GetGardenBuffList()
    {
        return gardenBuffList;
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void SaveBuffs()
    {
        saver.SaveBuffs();
    }
    
    public void LoadBuffs()
    {
        saver.LoadBuffs();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.ENEMY_KILLED, DropCoin);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.ENEMY_KILLED, DropCoin);
    }

    private void DropCoin(Dictionary<string, object> _)
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= GlobalGarden.CoinDropChance)
        {
            Vector2 dropPosition = new Vector2((float)_["x"], (float)_["y"]);
            Instantiate(ClefCoin, dropPosition, Quaternion.identity);
        }
    }

    public void BuffClicked(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (GardenBuff buff in gardenBuffList)
            {
                if (buff.IsSelected)
                {
                    buff.BaseLevelUp();
                    return;
                }
            }
        }
    }

    private void UpdateCoinUI()
    {
        coinText.text = GlobalGarden.Coins.ToString();
    }

    public void AddCoins(int _coins)
    {
        GlobalGarden.Coins += _coins;
        UpdateCoinUI();
    }


    private bool isRemoving = false;
    public void RemoveCoins(int _coins)
    {
        int startingCoins = GlobalGarden.Coins;
        GlobalGarden.Coins -= _coins;
        Managers.Instance.Resolve<IGardenBuffMng>().SaveBuffs();

        if (!isRemoving)
        {
            StartCoroutine(RemoveCoinAnim());
            
        }

        IEnumerator RemoveCoinAnim()
        {
            isRemoving = true;
            for (int i = 1; i <= _coins; i++)
            {
                coinText.text = (startingCoins - i).ToString();
                yield return new WaitForSeconds(0.01f);
            }
            isRemoving = false;
        }
    }
    
    public int GetCoins() 
    {
        return GlobalGarden.Coins;
    }

}
