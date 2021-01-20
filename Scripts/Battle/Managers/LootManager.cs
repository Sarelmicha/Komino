using Komino.CampaignBattle.Stats;
using Komino.Enums;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{

    [SerializeField] private InteractiveBar lootbar = null;
    [SerializeField] private CampaignBattleStats campaignBattleStats = null;
    [SerializeField] private GameObject allLoots = null;
    [SerializeField] private CampaignLoot lootPrefab = null;

    private int numOfStars = 0;

    private void Start()
    {
        lootbar.SetValue(0);
    }

    public IEnumerator GainLoot(int amount, int newMaxLuck, List<LootData> lootData)
    {
        
        yield return new WaitUntil(() => !lootbar.isInTheMiddleOfGainOrReduceValue);

        print("im in GainLuck and i passed WaitUntil");

        if (!lootbar.IsRestarted())
        {
            lootbar.Gain(amount, newMaxLuck, lootData);
        }
    }

    public void SetLoot(int amount)
    {
        lootbar.SetValue(amount);
    }


    public void ResetLootbar()
    {
        lootbar.Restart();
        ResetAllStars();
    }

    public void SetRestarted()
    {
        lootbar.IsRestarted(false);
    }

    public float GetLoot()
    {
        return lootbar.value;
    }

    public void SetLootbarData(AllTurns allTurns)
    {
        print("count is " + allTurns.player_turns.Count);
        lootbar.SetValue((float)allTurns.player_turns[allTurns.player_turns.Count - 1].all_turns_loot);
        lootbar.SetMaxValue((float)allTurns.loot_bars[allTurns.loot_bars.Count - 1].min);
    }

    private void ReachedTargetLoot()
    {
        InstantiateOneLoot(lootPrefab, campaignBattleStats.GetCurrentLootTarget(), true);
        numOfStars++;
    }

    private void ResetAllStars()
    {
        for (int i = 0; i < allLoots.transform.childCount; i++)
        {
            allLoots.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public int GetNumOfStars()
    {
        return this.numOfStars;
    }

    public void SetMaxLoot(float maxLuck)
    {
        lootbar.SetMaxValue(maxLuck);

    }

    public void OnLootbarReachedTarget()
    {
        ReachedTargetLoot();
        campaignBattleStats.IncrementCurrentTargetLootDataIndex(campaignBattleStats.GetLootBarsData().Count -1);

    }

    public void SetCheckpoints(List<LootData> loot_bars)
    {
        print("im in SetCheckpoints1 ");
        foreach (LootData lootData in loot_bars)
        {

            print("im in SetCheckpoints2 ");

            InstantiateOneLoot(lootPrefab, lootData, false);

        }
    }

    public void SetReachedTargeStars(double currentLoot, List<LootData> loot_bars)
    {
        foreach (LootData lootData in loot_bars)
        {
            print("im in SetReachedTargeStars ");
            if (currentLoot >= lootData.min)
            {
                print("im in InstantiateOneLoot ");
                InstantiateOneLoot(lootPrefab, lootData, true);
            }
        }
    }

    

    private void InstantiateOneLoot(CampaignLoot lootPrefab, LootData lootData, bool isReached)
    {
        print("im in InstantiateOneLoot ");
        float percetange = ((float)lootData.min / lootbar.GetMaxValue()) * 100;
        float sliderWidth = lootbar.GetComponent<RectTransform>().sizeDelta.x;
        float unitSize = sliderWidth / lootbar.GetMaxValue();


        float zeroValue = lootbar.transform.position.y - (sliderWidth / 2); //Get the leftmost corner of the slider.

        float valueToIncrement = ((float)lootData.min / lootbar.GetMaxValue()); //Get the % of the checkpoint based on the max value of the level.
        float newPos = (sliderWidth * valueToIncrement); //New position in screen

        print("width is " + sliderWidth);
        print("newPos = " + newPos);

        print("unit size = " + unitSize);

        print("percetange " + percetange);
        print("pos in y is " + allLoots.transform.position.y + (percetange * unitSize));
        print("min is " + lootData.min);

        Loot loot = new Loot(lootData, isReached);
        loot.IsReached(isReached);
        CampaignLoot newLoot = Instantiate(lootPrefab, new Vector2(allLoots.transform.position.x, zeroValue + newPos), Quaternion.identity) as CampaignLoot;
        newLoot.SetLoot(loot);
        newLoot.transform.SetParent(allLoots.transform);

    }

    public void SetLootbarActive(bool active)
    {
        lootbar.gameObject.SetActive(active);
    }
}
