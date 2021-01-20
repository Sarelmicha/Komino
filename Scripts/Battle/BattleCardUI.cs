using Komino.CampaignBattle.Cards.BattleCards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardUI : MonoBehaviour
{

    [SerializeField] Text displayNameText = null;
    [SerializeField] GameObject attackHolder = null;
    [SerializeField] GameObject luckHolder = null;
    [SerializeField] GameObject abilityHolder = null;
    [SerializeField] GameObject costHolder = null;
    [SerializeField] Image facePanelImage = null;
    [SerializeField] Image backPanelImage = null;
    [SerializeField] Image creatureImage = null;


    public void SetCardDisplay(BattleCard battleCard)
    {

        if (battleCard.battleCardData == null)
        {
            return;
        }

        // Set card text
        if (displayNameText != null)
        {
            displayNameText.text = battleCard.battleCardData.display_name;
        }


        // Set card sprite
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


        if (attackHolder != null)
        {
            if (battleCard.battleCardData.attack == 0)
            {
                print("im here so attack is 0");
                this.attackHolder.SetActive(false);
            }
            else
            {
                print("im here so attack is is " + battleCard.battleCardData.attack);
                this.attackHolder.GetComponentInChildren<Text>().text = battleCard.battleCardData.attack.ToString();
            }
        }

        if (luckHolder != null)
        {
            if (battleCard.battleCardData.loot == 0)
            {
                print("im here so attack is 0");
                this.luckHolder.SetActive(false);
            }

            else
            {
                print("im here so luck is is " + battleCard.battleCardData.loot);
                this.luckHolder.GetComponentInChildren<Text>().text = battleCard.battleCardData.loot.ToString();
            }
        }

        if (abilityHolder != null)
        {
            if (battleCard.battleCardData.abilities == null)
            {
                this.abilityHolder.SetActive(false);
            }

            //this.abilityHolder.GetComponentInChildren<Text>().text = battleCard.battleCardData.abilities[0];
            if (battleCard.abilitySprtie != null)
            {
                this.abilityHolder.transform.GetChild(0).GetComponent<Image>().sprite = battleCard.abilitySprtie;
            }
        }

        if (costHolder != null)
        {
            if (battleCard.battleCardData.cost == 0)
            {
                this.costHolder.SetActive(false);

            }

            else
            {
                this.costHolder.GetComponentInChildren<Text>().text = battleCard.battleCardData.cost.ToString();

            }
        }
    }


    public Image GetCreatureImage()
    {
        return this.creatureImage;
    }

 
}
