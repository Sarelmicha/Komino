using Komino.CampaignBattle.Cards.BattleCards;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoUI : MonoBehaviour
{
    [SerializeField] Text displayNameText = null;
    [SerializeField] Image facePanelImage = null;
    [SerializeField] Image backPanelImage = null;
    [SerializeField] Image creatureImage = null;
    [SerializeField] Text teirText = null;

    [SerializeField] Text currentAttackLevelText = null;
    [SerializeField] Text currentLevelLuckText = null;
    [SerializeField] Text currentLevelCritChanceText = null;
    [SerializeField] Text currentLevelCritDamageText = null;
    [SerializeField] Text currentLevelTextlevelText = null;
    [SerializeField] Text currentPowerLevelText = null;
    [SerializeField] Text currentChanceToAppearInDeckText = null;


    [SerializeField] Text nextAttackLevelText = null;
    [SerializeField] Text nextLevelLuckText = null;
    [SerializeField] Text nextLevelCritChanceText = null;
    [SerializeField] Text nextLevelCritDamageText = null;
    [SerializeField] Text nextPowerLevelText = null;
    [SerializeField] Text nextChanceToAppearInDeckText = null;

    [SerializeField] Text targetFood = null;
    [SerializeField] Text playerSculptures = null;
    [SerializeField] Text targetSculptures = null;

    [SerializeField] Slider sculpturesSlider = null;


    public void SetCardDisplay(BattleCard battleCard)
    {

        //Set card text
        if (displayNameText != null)
        {
            displayNameText.text = battleCard.battleCardData.display_name;
        }


        //Set card sprite
        if (battleCard.facePanelSprite != null)
        {
            facePanelImage.sprite = battleCard.facePanelSprite;

        }

        if (battleCard.creatureSprite != null)
        {
            creatureImage.sprite = battleCard.creatureSprite;
        }

        if (battleCard.backPanelSprite != null)
        {
            backPanelImage.sprite = battleCard.backPanelSprite;
        }

        if (teirText != null)
        {
            this.teirText.text = battleCard.battleCardData.tier.ToString();
        }

        SetCurrentValue(currentAttackLevelText, battleCard.battleCardData.levels, "attack");
        SetCurrentValue(currentLevelLuckText, battleCard.battleCardData.levels, "luck");
        SetCurrentValue(currentLevelCritChanceText, battleCard.battleCardData.levels, "critical_hit_chance");
        SetCurrentValue(currentLevelCritDamageText, battleCard.battleCardData.levels, "critical_hit_damage");
        SetCurrentValue(currentLevelTextlevelText, battleCard.battleCardData.levels, "level");
        SetCurrentValue(currentPowerLevelText, battleCard.battleCardData.levels, "power");
        SetCurrentValue(currentChanceToAppearInDeckText, battleCard.battleCardData.levels, "chance_to_appear_in_deck");
        SetNextValue(nextAttackLevelText, battleCard.battleCardData.levels, "attack");
        SetNextValue(nextLevelLuckText, battleCard.battleCardData.levels, "luck");
        SetNextValue(nextLevelCritChanceText, battleCard.battleCardData.levels, "critical_hit_chance");
        SetNextValue(nextLevelCritDamageText, battleCard.battleCardData.levels, "critical_hit_damage");
        SetNextValue(nextPowerLevelText, battleCard.battleCardData.levels, "power");
        SetNextValue(nextChanceToAppearInDeckText, battleCard.battleCardData.levels, "chance_to_appear_in_deck");

        SetSculpturesSlider(battleCard);



        if (targetFood != null)
        {

            this.targetFood.text = battleCard.battleCardData.levels[1].cost.coins.ToString();

        }
 
    }

    private void SetSculpturesSlider(BattleCard battleCard)
    {
        if (targetSculptures != null)
        {
           
            sculpturesSlider.maxValue = (float)battleCard.battleCardData.levels[1].cost.sculptures;
            sculpturesSlider.value = (float)battleCard.battleCardData.player_card.sculptures;

            this.playerSculptures.text = "" + sculpturesSlider.value;
            this.targetSculptures.text = "/ " + sculpturesSlider.maxValue;


        }
    }

    public void SetCreatureImageColor(Color color)
    {
        creatureImage.color = color;
    }

    private void SetCurrentValue(Text currentText, List<CardLevelData> levels, string property)
    {

        Debug.Log("levels is " + levels);
        Debug.Log("currentText = " + currentText);

        if (currentText == null)
        {
            return;
        }
        int index = levels[0] != null ? 0 : 1;

        Debug.Log("index = " + index);
        Debug.Log("pproperty = " + property);
        Debug.Log("type = " + levels[index].GetType());
        Debug.Log(levels[index].GetType().GetProperty(property));

        currentText.text = levels[index].GetType().GetProperty(property).GetValue(levels[index]).ToString();
    }

    private void SetNextValue(Text currentText, List<CardLevelData> levels, string property)
    {

        if (currentText == null)
        {
            return;
        }

        if (levels[1] == null || levels[0] == null)
        {
            currentText.enabled = false;
            return;
        }

        print("--------------------------------------------------");
        print(levels[1].GetType().GetProperty(property).GetValue(levels[1]) + "and type is " + levels[1].GetType().GetProperty(property).GetValue(levels[1]).GetType());
        print(levels[0].GetType().GetProperty(property).GetValue(levels[0]) + "and type is " + levels[0].GetType().GetProperty(property).GetValue(levels[0]).GetType());
        print("property is " + property);

        if ((int)((double)levels[1].GetType().GetProperty(property).GetValue(levels[1])) - (int)((double)levels[0].GetType().GetProperty(property).GetValue(levels[0])) != 0)
        {
            currentText.text = "+" + ((int)((double)levels[1].GetType().GetProperty(property).GetValue(levels[1])) - (int)((double)levels[0].GetType().GetProperty(property).GetValue(levels[0])));
        }

        else
        {
            currentText.enabled = false;
        }
    }


}
