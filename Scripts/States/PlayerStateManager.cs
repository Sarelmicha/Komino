using AppState.Player;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState = null;
    //Events 
    [SerializeField] private ValueEvent onTotalCoinsUpdated = null;
    [SerializeField] private ValueEvent onTotalGemsUpdated = null;

    private void Awake()
    {
        GameObject[] playerStateManagers = GameObject.FindGameObjectsWithTag("playerStateManager");

        if (playerStateManagers.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetCards(List<CardData> cards)
    {
        playerState.Cards = cards;
    }

    public List<CardData> GetCards()
    {
        return playerState.Cards;
    }

    public void SetPower(int power)
    {
        playerState.Power = power;
    }

    public double GetPower()
    {
        return playerState.Power;
    }

    public void SetFame(int fame)
    {
        playerState.Fame = fame;
    }

    public double GetFame()
    {
        return playerState.Fame;
    }

    public void SetCoins(Value coins)
    {
        print("coins to set is " + coins.GetValue());
        playerState.Coins = coins.GetValue();
        Debug.Log("OnTotalManaUpdated has been raised!");
        onTotalCoinsUpdated.Raise(coins);
    }

    public double GetCoins()
    {
        return playerState.Coins;
    }

    public void SetGems(Value gems)
    {
        print("gems to set is " + gems.GetValue());
        playerState.Gems = gems.GetValue();
        Debug.Log("OnTotalManaUpdated has been raised!");
        onTotalGemsUpdated.Raise(gems);
    }

    public double GetGems()
    {
        return playerState.Gems;
    }
}
