using AppState.Game;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHUDManager : HUDManager
{
    [SerializeField] private Text hoursLeftNewChests = null;
    [SerializeField] private Text minutesLeftNewChests = null;
    [SerializeField] private Text numOfGemsToRefresh = null;

    [SerializeField] private GameState gameState = null;

    private Value hours = null;
    private Value minutes = null;
    private Value gems = null;
    private (int, int, int) chestsData;
    private bool chestsHasBeenRefreshed = false;

    private void Awake()
    {
        hours = new Value(0);
        minutes = new Value(0);
        gems = new Value(0);
    }

    private void Start()
    {
        ShowRefreshChestsData();
    }

    public void ShowRefreshChestsData()
    {
        chestsData = gameState.GetChestsData();
        StartCoroutine(UpdateRefreshChestData(chestsData.Item1, chestsData.Item2));
    }

    private IEnumerator UpdateRefreshChestData(int seconds, int gems)
    {
        print("seconds left is " + seconds);
        ConvertSecondsToHourAndMinutes(seconds);

        print("hours = " + hours.GetValue());
        print("minutes = " + minutes.GetValue());

        do
        {
            hoursLeftNewChests.text = hours.GetValue() + "H";
            minutesLeftNewChests.text = minutes.GetValue() + "M";
            numOfGemsToRefresh.text = gems.ToString();

            yield return new WaitForSeconds(60f);
         
            minutes.SetValue(minutes.GetValue() - 1);
            if (minutes.GetValue() == 0)
            {
                hours.SetValue(hours.GetValue() - 1);
            }
            
        } while ((hours.GetValue() != 0 && minutes.GetValue() != 0) || !chestsHasBeenRefreshed);

        //Reset the chestsHasBenRefreshed for next round.
        chestsHasBeenRefreshed = false;

    }

    private void ConvertSecondsToHourAndMinutes(int seconds)
    {
        minutes.SetValue(seconds % 60);
        hours.SetValue(seconds / 3600);
    }

    private IEnumerator UpdateNumOfGemsToRefresh(int numOfGems)
    {
        yield return new WaitForSeconds(60f);
    }

    public void OnChestsHasBeenRefreshed()
    {
        chestsHasBeenRefreshed = true;
        StartCoroutine(ShowRefreshChestsDataCorutine());
    }

    public IEnumerator ShowRefreshChestsDataCorutine()
    {
        yield return new WaitUntil(() => chestsHasBeenRefreshed == false);
        ShowRefreshChestsData();
    }
}
