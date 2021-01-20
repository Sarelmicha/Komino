using AppState.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatHUDManager : HUDManager
{
    [SerializeField] private Text totalGemsText = null;
    [SerializeField] private Text totalCoinsText = null;
    [SerializeField] private Text playerName = null;
    [SerializeField] private Text playerLevel = null;

    [SerializeField] private float fastDelayBetweenIncrement = 0.0005f;
    [SerializeField] private float slowDelayBetweenIncrement = 0.05f;
    [SerializeField] private PlayerState playerState = null;

    private Value totalGems = null;
    private Value totalCoins = null;

    private void Awake()
    {
        totalGems = new Value((int)playerState.Gems);
        totalCoins = new Value((int)playerState.Coins);

        UpdateTotalCoinsText(new Value((int)playerState.Coins,true));
        UpdateTotalGemsText(new Value((int)playerState.Gems, true));
        playerName.text = playerState.DisplayName;
        playerLevel.text = playerState.Level.ToString();

    }

    // Called by listener
    public void UpdateTotalGemsText(Value targetTotalGems)
    {

        print("update food has been calleed");

        if (targetTotalGems.SetInstantly())
        {

            SetTextInstantly(totalGemsText, totalGems, targetTotalGems.GetValue(), "");
            return;
        }

        if (totalGems.GetValue() < targetTotalGems.GetValue())
        {
            StartCoroutine(IncrementToTarget(totalGemsText, totalGems, targetTotalGems.GetValue(), "", fastDelayBetweenIncrement, 10));
            return;
        }

        StartCoroutine(DecrementToTarget(totalGemsText, totalGems, targetTotalGems.GetValue(), "", fastDelayBetweenIncrement, 10));

    }

    // Called by listener
    public void UpdateTotalCoinsText(Value targetTotalCoins)
    {
        print("im here in UpdateTotalManaText and set instant is " + targetTotalCoins.SetInstantly());

        if (targetTotalCoins.SetInstantly())
        {

            SetTextInstantly(totalCoinsText, totalCoins, targetTotalCoins.GetValue(), "");
            return;
        }


        print("total mana is " + totalCoins.GetValue());
        print("targetTotalMana is " + targetTotalCoins.GetValue());

        if (totalCoins.GetValue() < targetTotalCoins.GetValue())
        {
            StartCoroutine(IncrementToTarget(totalCoinsText, totalCoins, targetTotalCoins.GetValue(), "", fastDelayBetweenIncrement, 10));
            return;
        }

        print("let decrement!");
        StartCoroutine(DecrementToTarget(totalCoinsText, totalCoins, targetTotalCoins.GetValue(), "", fastDelayBetweenIncrement, 10));

    }
}
