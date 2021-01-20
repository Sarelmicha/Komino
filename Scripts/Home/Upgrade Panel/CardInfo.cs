using Komino.CampaignBattle.Cards.BattleCards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    private Canvas canvas = null;
    private CanvasGroup canvasGroup = null;
    private CardInfoUI cardInfoUI = null;

    [SerializeField] Button upgradeButton = null;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        cardInfoUI = GetComponent<CardInfoUI>();
    }

    public void Show(BattleCard pickedCard , bool isPlayerHaveThisCard,bool isPlayerHaveEnoughCurriences)
    {
        if (isPlayerHaveThisCard)
        {
            cardInfoUI.SetCreatureImageColor(Color.white);
            upgradeButton.gameObject.SetActive(true);

            if (!isPlayerHaveEnoughCurriences)
            {
                upgradeButton.interactable = false;
            }

            else
            {
                upgradeButton.interactable = true;
            }
        }
        else
        {
            cardInfoUI.SetCreatureImageColor(Color.black);
            //Remove update button from card
            upgradeButton.gameObject.SetActive(false);

        }

        cardInfoUI.SetCardDisplay(pickedCard);
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
        canvasGroup.blocksRaycasts = false;
        
    }

}
