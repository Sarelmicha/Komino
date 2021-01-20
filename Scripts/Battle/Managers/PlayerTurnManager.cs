using Komino.CampaignBattle.Cards.BattleCards;
using Komino.CampaignBattle.Stats;
using Komino.Enums;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnManager : MonoBehaviour, ITurnManager
{

    [SerializeField] private GridLayoutGroup cardsHolder = null;

    [SerializeField] private CampaignBattleCard campaignBattleCardPrefab = null;
    [SerializeField] private CampaignBattleStats campaignBattleStats = null;
    [SerializeField] private AbilitiesManager abilitiesManager = null;

    [SerializeField] private Transform center = null;
    [SerializeField] private Transform dealer = null;
    [SerializeField] private Transform bombHolder = null;

    [SerializeField] private VoidEvent onBombSummoned = null;
    [SerializeField] private VoidEvent OnWinningCardSummoned = null;
    [SerializeField] private CampaignBattleCardEvent onFinishedHandlePlayerTurn = null;


    private BattleCardHelper battleCardHelper = null;
    // Helpers

    private int numOfCardsOnDeck = 0;
    private bool summonToCenter = false;
    private bool hasDefuseAbility = false;
    private bool hasWinAbility = false;




    private void Awake()
    {
        battleCardHelper = new BattleCardHelper();
    }


    public void HandleOneTurn(Turn turn, string choice, List<BattleCard> battleCards)
    {
        print("turn.card = " + turn.card);
        battleCards.Add(battleCardHelper.CreateBattleCard(turn.card));

        print("lets summon!");

        SummonCard(turn,choice);

    }



    private void SummonCard(Turn turn, string choice)
    {

        CampaignBattleCard newCampaignBattleCard = Instantiate(campaignBattleCardPrefab, cardsHolder.transform.position, Quaternion.identity) as CampaignBattleCard;

        if (!newCampaignBattleCard)
        {
            print("New battle card display is null");
            return;
        }

        // Set the player type
        newCampaignBattleCard.SetPlayerType(PlayerType.Player);

        HandleCard(turn, newCampaignBattleCard, choice);

    }

    private void HandleCard(Turn turn, CampaignBattleCard newCampaignBattleCard,string choice)
    {
        BattleCard battleCard = CreateBattleCardAccordingToChoice(turn,choice);
        newCampaignBattleCard.SetBattleCard(battleCard);
        newCampaignBattleCard.SetAmount(battleCardHelper.GetCardComboAmount(turn.combos, newCampaignBattleCard.GetBattleCard().battleCardData));
        abilitiesManager.InvokeAbility(this, turn, newCampaignBattleCard);
        newCampaignBattleCard.TriggerFlipWithScaleAnimation();
        newCampaignBattleCard.SetBattleCardRectTransform(cardsHolder.transform);
        StartCoroutine(OnCardFinishedSummon(newCampaignBattleCard));
    }

    private BattleCard CreateBattleCardAccordingToChoice(Turn turn, string choice)
    {

        BattleCard battleCard = campaignBattleStats.GetPlayerBattleCards()[numOfCardsOnDeck++];

        if (choice == ChoicesConstants.DAMAGE)
        {

            battleCard.battleCardData.attack = turn.choices.damage;
            battleCard.battleCardData.loot = 0;
            battleCard.battleCardData.abilities = null;
        }

        else if (choice == ChoicesConstants.LOOT)
        {
            battleCard.battleCardData.attack = 0;
            battleCard.battleCardData.loot = turn.choices.loot;
            battleCard.battleCardData.abilities = null;
        }

        else if (choice == ChoicesConstants.CRIT)
        {
            if (turn.loot != 0)
            {
                //luck crit has been invoked
                battleCard.battleCardData.attack = 0;
                battleCard.battleCardData.loot = turn.loot;
                battleCard.battleCardData.abilities = null;
            }

            else if (turn.damage != 0)
            {
                //damage crit has been invoked
                battleCard.battleCardData.attack = turn.damage;
                battleCard.battleCardData.loot = 0;
                battleCard.battleCardData.abilities = null;
            }

            else
            {
                battleCard.battleCardData.attack = 0;
                battleCard.battleCardData.loot = 0;
                battleCard.battleCardData.abilities = null;
            }

        }

        else if (choice == ChoicesConstants.ABILITY)
        {
            battleCard.battleCardData.attack = 0;
            battleCard.battleCardData.loot = 0;
            //TODO - complete this when ability is send from server
            battleCard.battleCardData.abilities = null;
        }

        else if (choice == ChoicesConstants.SAVE)
        {
            battleCard.battleCardData.attack = 0;
            battleCard.battleCardData.loot = 0;
            battleCard.battleCardData.abilities = null;
        }

        return battleCard;
    }

    private IEnumerator OnCardFinishedSummon(CampaignBattleCard campaignBattleCard)
    {
        campaignBattleCard.TriggerIdleAnimation();

      
    
        if (campaignBattleCard.IsWinningCard())
        {
            print("OnWinningCardSummoned has been raised!");
            OnWinningCardSummoned.Raise();

            yield break;
        }

        if (campaignBattleCard.IsJoker())
        {
            //Flip fast and change sprtie to creature sprite
            campaignBattleCard.GetBattleCard().SetCreature(campaignBattleCard.GetBattleCard().battleCardData.nation, campaignBattleCard.GetBattleCard().battleCardData.identifier_name);
            campaignBattleCard.GetBattleCardUI().SetCardDisplay(campaignBattleCard.GetBattleCard());
        }

        else
        {
            //Regular card
            yield return new WaitForSeconds(0.5f);
        }




        // Summon turn prizes
        //*******************************************************
        //prizeManager.InstantiatePrizes(turnPrizes,true);
        //*******************************************************

        //Notity that the turn handle finished
        onFinishedHandlePlayerTurn.Raise(campaignBattleCard);
    }




    public void SetCards(List<Turn> player_turns)
    {

        for (int i = 0; i < player_turns.Count; i++)
        {
            CampaignBattleCard newCampaignBattleCard = SetCard(player_turns[i]);
            //if (i == player_turns.Count - 1)
            //{
            //    onFinishedHandlePlayerTurn.Raise(newCampaignBattleCard);
            //}
        }
    }

    private CampaignBattleCard SetCard(Turn turn)
    {
        CampaignBattleCard newCampaignBattleCard = Instantiate(campaignBattleCardPrefab, Vector3.zero, Quaternion.identity) as CampaignBattleCard;
        newCampaignBattleCard.SetPlayerType(PlayerType.Player);
        BattleCard battleCard = CreateBattleCardAccordingToChoice(turn, turn.choice);
        newCampaignBattleCard.SetBattleCard(battleCard);
        newCampaignBattleCard.SetAmount(battleCardHelper.GetCardComboAmount(turn.combos, newCampaignBattleCard.GetBattleCard().battleCardData));
        newCampaignBattleCard.SetBattleCardRectTransform(cardsHolder.transform);
        newCampaignBattleCard.TriggerOpenNoFlipAnimation();
        return newCampaignBattleCard;
    }



    public void RestartGame()
    {
        RestartBoard();
        summonToCenter = false;
    }

    public void RestartBoard()
    {
        DestoryCards();
        numOfCardsOnDeck = 0;
        hasDefuseAbility = false;
        hasWinAbility = false;
    }


    private void DestoryCards()
    {
        for (int i = 0; i < campaignBattleStats.GetNumOfPlayerCards(); i++)
        {
            Destroy(cardsHolder.transform.GetChild(i).gameObject);
            //Destroy(cardsHolder.transform.GetChild(i).transform.GetChild(0).gameObject);
        }
    }




    public void HasDefuseAbility(bool hasDefuseAbility)
    {
        this.hasDefuseAbility = hasDefuseAbility;
    }

    public bool HasDefuseAbility()
    {
        return this.hasDefuseAbility;
    }

    public void HasWinAbility(bool hasWinAbility)
    {
        this.hasWinAbility = hasWinAbility;
    }

    public bool HasWinAbility()
    {
        return this.hasWinAbility;
    }


    public void DestroyLastCard()
    {
        Destroy(cardsHolder.transform.GetChild(cardsHolder.transform.childCount - 1).gameObject);
    }

    public int GetNumOfCardsOnDeck()
    {
        return this.numOfCardsOnDeck;
    }

   
}
