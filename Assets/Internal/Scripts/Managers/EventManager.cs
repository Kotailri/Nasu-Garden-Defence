using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class EventStrings
{
    public static readonly string ENEMY_DELETED      = "ENEMY_DELETED";
    public static readonly string ENEMY_KILLED       = "ENEMY_KILLED";
    public static readonly string ENEMY_HIT          = "ENEMY_HIT";
    public static readonly string PLAYER_ATTACK      = "PLAYER_ATTACK";
    public static readonly string STATS_UPDATED      = "STATS_UPDATED";
    public static readonly string PLAYER_TAKE_DAMAGE = "PLAYER_TAKE_DAMAGE";
    public static readonly string GAME_OVER_KILL_ALL = "GAME_OVER_EVENT";
    public static readonly string WAVE_END = "WaVE_ENDED";

    public static readonly string GAME_RESET = "GAME_RESET";
    public static readonly string GAME_START = "GAME_START";

    // Controls
    public static readonly string INTERACT_PRESSED = "INTERACT_BUTTON_PRESSED";
}

public static class EventManager
{
    private static Dictionary<string, Action<Dictionary<string, object>>> _eventDictionary = new();

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;

        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            _eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            _eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            _eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(message);
        }
        else
        {
            Debug.WriteLine("Generic Event Manager triggered an event that caused an error.");
        }
    }
}

// ========== EXAMPLE ============
/*

EventManager.TriggerEvent("gameOver", null);
EventManager.TriggerEvent("addReward", new Dictionary<string, object> {
    { "name", "candy" },
    { "amount", 5 }  
});

void OnEnable()
{
    EventManager.StartListening("addCoins", OnAddCoins);
}

void OnDisable()
{
    EventManager.StopListening("addCoins", OnAddCoins);
}

void OnAddCoins(Dictionary<string, object> message)
{
    var amount = (int)message["amount"];
    coins += amount;
}
*/
