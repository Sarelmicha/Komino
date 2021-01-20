using Komino.CampaignBattle.Stats;
using Komino.Core;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class ManaPerTurnManager : MonoBehaviour
{
    [SerializeField] GameObject summonHolder = null;
    [SerializeField] Slider manaSlider = null;
    [SerializeField] Button plusButton = null;
    [SerializeField] Button minusButton = null;
    [SerializeField] Text summonCostText = null;
    [SerializeField] IntEvent onManaPerTurnUpdated = null;
    [SerializeField] GameErrorHandler gameErrorHandler = null;
    [SerializeField] CampaignBattleStats campaignBattleStats = null;

    private float summonCost;


    public void onSliderValueChanged()
    {
        summonCost = manaSlider.value;
        summonCostText.text = summonCost.ToString();
        UpdateButtonsState();
    }


    public void HandleSummon(ManaInfo manaInfo)
    {
        //Set the summon window active
        summonHolder.SetActive(true);
        // Set min and max value
        manaSlider.minValue = manaInfo.min;
        manaSlider.maxValue = manaInfo.max;
        //Set summonCost to min value
        summonCost = manaSlider.minValue;
        manaSlider.value = summonCost;
        //Set the text of summonCost
        summonCostText.text = summonCost.ToString();
        //Set minusButton to uniteractble
        minusButton.interactable = false;

    }
    // Called when click on Plus Button from UI
    public void OnPlusButtonClicked()
    {
        manaSlider.value += 1;        
        UpdateButtonsState();
    }

    // Called when click on Minus Button from UI
    public void OnMinusButtonClicked()
    {
        manaSlider.value -= 1;
        UpdateButtonsState();
    }

    // Called when click on Confirm Button from UI
    public void OnConfrimButtonClicked()
    {
        if (campaignBattleStats.GetTotalMana() - manaSlider.value < 0)
        {
            gameErrorHandler.HandleError(GameError.NOT_ENOUGH_MANA, null);
            return;
        }

        summonHolder.SetActive(false);
        onManaPerTurnUpdated.Raise((int)manaSlider.value);
       
    }

    // Called when click on All In Button from UI
    public void OnAllInButtonClicked()
    {
       
        if (campaignBattleStats.GetTotalMana() - manaSlider.value < 0)
        {
            gameErrorHandler.HandleError(GameError.NOT_ENOUGH_MANA, null);
            return;
        }

        manaSlider.value = manaSlider.maxValue;
        summonHolder.SetActive(false);
        onManaPerTurnUpdated.Raise((int)manaSlider.maxValue);
       
    }

    private void UpdateButtonsState()
    {
        if (summonCost == manaSlider.minValue)
        {
            minusButton.interactable = false;
        }
        else if (summonCost == manaSlider.maxValue)
        {
            plusButton.interactable = false;
        }

        else
        {
            minusButton.interactable = true;
            plusButton.interactable = true;
        }
    }

}
*/
