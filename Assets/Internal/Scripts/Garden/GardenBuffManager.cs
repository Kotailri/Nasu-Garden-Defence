using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GardenBuffManager : MonoBehaviour
{
    public GameObject ClefCoin;
    public TextMeshProUGUI coinText;

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
        Vector2 dropPosition = new Vector2((float)_["x"], (float)_["y"]);
        Instantiate(ClefCoin, dropPosition, Quaternion.identity);
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

    public void RemoveCoins(int _coins)
    {
        Coins -= _coins;
        UpdateCoinUI();
    }
    
    public int GetCoins() 
    {
        return Coins;
    }

}
