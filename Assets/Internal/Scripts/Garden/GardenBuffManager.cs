using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GardenBuffManager : MonoBehaviour
{
    public GameObject ClefCoin;
    public TextMeshProUGUI coinText;

    [Header("Buffs")]
    public List<GardenBuff> gardenBuffList = new();

    private int Coins = 0;

    private void Awake()
    {
        Global.gardenBuffManager = this;
    }

    private void Start()
    {
        Coins = GlobalGarden.Coins;
        UpdateCoinUI();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.ENEMY_KILLED, DropCoin);
    }

    private void OnDisable()
    {
        GlobalGarden.Coins = Coins;
        EventManager.StopListening(EventStrings.ENEMY_KILLED, DropCoin);
    }

    private void DropCoin(Dictionary<string, object> _)
    {
        if (Random.Range(0f,1f) <= GlobalGarden.CoinDropChance)
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
        coinText.text = Coins.ToString();
    }

    public void AddCoins(int _coins)
    {
        Coins += _coins;
        UpdateCoinUI();
    }


    private bool isRemoving = false;
    public void RemoveCoins(int _coins)
    {
        if (isRemoving)
        {
            Coins -= _coins;
        }
        else
        {
            StartCoroutine(RemoveCoinAnim());
        }

        IEnumerator RemoveCoinAnim()
        {
            isRemoving = true;
            for (int i = 0; i < _coins; i++)
            {
                Coins -= 1;
                UpdateCoinUI();
                yield return new WaitForSeconds(0.01f);
            }
            isRemoving = false;
        }
    }
    
    public int GetCoins() 
    {
        return Coins;
    }

}
