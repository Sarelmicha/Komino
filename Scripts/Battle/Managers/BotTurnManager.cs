using Komino.CampaignBattle.Cards;
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

public class BotTurnManager : MonoBehaviour, ITurnManager
{

    [SerializeField] private GridLayoutGroup cardsHolder = null;

    [SerializeField] private CampaignBattleCard campaignBattleCardPrefab = null;
    [SerializeField] private Transform center = null;

    [SerializeField] private CampaignBattleStats campaignBattleStats = null;

    // Events
    [SerializeField] private VoidEvent OnWinningCardSummoned = null;
    [SerializeField] private CampaignBattleCardEvent onFinishedHandleEnemyTurn = null;


    [SerializeField] private AbilitiesManager abilitiesManager = null;


    private bool hasDefuseAbility = false;
    private bool hasWinAbility = false;

    private int numOfCardsOnDeck = 0;
    private int numOfFlipedCard = 0;
    //private bool isGameStarted = false;
    private BattleCardHelper battleCardHelper = null;

    private void Awake()
    {
        battleCardHelper = new BattleCardHelper();
    }

    public void HandleOneTurn(Turn turn, string choice, List<BattleCard> battleCards)
    {
        battleCards.Add(battleCardHelper.CreateBattleCard(turn == null? null : turn.card));

        if (turn == null)
        {
            StartCoroutine(SummonFlipedCard());
            return;
        }

        SummonCard(turn, choice);
    }


    private void SummonCard(Turn turn, string choice)
    {
        // Create new battle card
        CampaignBattleCard newBattleCardController = Instantiate(campaignBattleCardPrefab, cardsHolder.transform.position, Quaternion.identity) as CampaignBattleCard;

        // Set the player type
        newBattleCardController.SetPlayerType(PlayerType.Enemy);

        HandleCard(turn, choice, newBattleCardController);
    }



    private void HandleCard(Turn turn, string choice, CampaignBattleCard newBattleCardController)
    {

        print("numOfCardsOnDeck = " + numOfCardsOnDeck);
        BattleCard battleCard = campaignBattleStats.GetEnemyBattleCards()[numOfCardsOnDeck];

        battleCard.battleCardData.attack = 0;
        battleCard.battleCardData.loot = 0;
        battleCard.battleCardData.abilities = null;

        // Set battle card info
        newBattleCardController.SetBattleCard(battleCard);
        newBattleCardController.SetAmount(battleCardHelper.GetCardComboAmount(turn.combos, newBattleCardController.GetBattleCard().battleCardData));
        // Invoke card avility
        abilitiesManager.InvokeAbility(this, turn, newBattleCardController);
        //newBattleCardController.TriggerBotFlipBotAnimation();
        newBattleCardController.TriggerFlipWithScaleAnimation();
        newBattleCardController.SetBattleCardRectTransform(cardsHolder.transform);

       OnCardFinishedSummon(newBattleCardController);

    }

    public void OnCardFinishedSummon(CampaignBattleCard campaignBattleCard)
    {

        print("im in OnCardFinishedSummon");
        // finished summon animation
        campaignBattleCard.TriggerIdleAnimation();

        if (campaignBattleCard.IsWinningCard())
        {
            print("OnWinningCardSummoned has been raised!");
            OnWinningCardSummoned.Raise();
        }

        if (campaignBattleCard.IsJoker())
        {
            //Flip fast and change sprtie to creature sprite
            campaignBattleCard.GetBattleCard().SetCreature(campaignBattleCard.GetBattleCard().battleCardData.nation, campaignBattleCard.GetBattleCard().battleCardData.identifier_name);
            campaignBattleCard.GetComponent<BattleCardUI>().SetCardDisplay(campaignBattleCard.GetBattleCard());
        }
        print("onFinishedHandleEnemyTurn has been raised! sarel");
        onFinishedHandleEnemyTurn.Raise(campaignBattleCard);
        numOfCardsOnDeck++;
    }

    public int GetNumOfFlipedCard()
    {
        return this.numOfFlipedCard;
    }


    public void SetCardsData(AllTurns allTurns)
    {
        for (int i = 0; i < cardsHolder.transform.childCount; i++)
        {
            CampaignBattleCard campaignBattleCard = cardsHolder.transform.GetChild(i).GetComponentInChildren<CampaignBattleCard>()
                as CampaignBattleCard;

            if (campaignBattleCard == null)
            {
                return;
            }

            campaignBattleCard.SetBattleCard(battleCardHelper.CreateBattleCard(allTurns.bot_turns[i].card));
            campaignBattleCard.SetAmount(battleCardHelper.GetCardComboAmount(allTurns.bot_turns[i].combos, campaignBattleCard.GetBattleCard().battleCardData));
        }
    }


    public void SetCards(List<Turn> bot_turns)
    {

        for (int i = 0; i < bot_turns.Count; i++)
        {          
            SetCard(bot_turns[i]);               
        }
    }

    private void SetCard(Turn turn)
    {
        CampaignBattleCard newCampaignBattleCard = Instantiate(campaignBattleCardPrefab, Vector3.zero, Quaternion.identity) as CampaignBattleCard;
        newCampaignBattleCard.SetPlayerType(PlayerType.Enemy);
        BattleCard battleCard = campaignBattleStats.GetEnemyBattleCards()[numOfCardsOnDeck++];
        newCampaignBattleCard.SetBattleCardRectTransform(cardsHolder.transform);
 
        if (battleCard.battleCardData != null)
        {
            battleCard.battleCardData.attack = 0;
            battleCard.battleCardData.loot = 0;
            battleCard.battleCardData.abilities = null;
            newCampaignBattleCard.SetBattleCard(battleCard);
            newCampaignBattleCard.SetAmount(battleCardHelper.GetCardComboAmount(turn.combos, newCampaignBattleCard.GetBattleCard().battleCardData));
            newCampaignBattleCard.TriggerFlipWithNoScaleAnimation();
            return;
        }

        newCampaignBattleCard.SetBattleCard(battleCard);
        numOfFlipedCard++;
    }

    public IEnumerator SummonFlipedCard()
    {

        CampaignBattleCard newCampaignBattleCard = Instantiate(campaignBattleCardPrefab, Vector3.zero, Quaternion.identity) as CampaignBattleCard;
        newCampaignBattleCard.SetPlayerType(PlayerType.Enemy);
        newCampaignBattleCard.SetBattleCardRectTransform(cardsHolder.transform);
        newCampaignBattleCard.TriggerCloseAnimation();
        yield return new WaitForSeconds(newCampaignBattleCard.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
        onFinishedHandleEnemyTurn.Raise(newCampaignBattleCard);
        numOfCardsOnDeck++;
        numOfFlipedCard++;
    }

    //private void SetCard()
    //{
    //    CampaignBattleCard newCampaignBattleCard = Instantiate(campaignBattleCardPrefab, Vector3.zero, Quaternion.identity) as CampaignBattleCard;
    //    newCampaignBattleCard.SetPlayerType(PlayerType.Enemy);
    //    newCampaignBattleCard.SetBattleCard(campaignBattleStats.GetEnemyBattleCards()[numOfCardsOnDeck++]);
    //    newCampaignBattleCard.SetBattleCardRectTransform(cardsHolder.transform);
    //}

    public void SetCloseCard(Turn turn)
    {
        for (int i = 0 ; i < cardsHolder.transform.childCount; i++)
        {

            print("im here4");

            CampaignBattleCard campaignBattleCard = cardsHolder.transform.GetChild(i).GetComponent<CampaignBattleCard>();
   
            if (campaignBattleCard.GetBattleCard() == null || campaignBattleCard.GetBattleCard().battleCardData == null)
            {
                print("im here in SetCloseCard");
                BattleCard battleCard = new BattleCard(turn.card);
                battleCard.battleCardData.attack = 0;
                battleCard.battleCardData.loot = 0;
                battleCard.battleCardData.loot = 0;
                battleCard.battleCardData.abilities = null;
                campaignBattleCard.SetBattleCard(battleCard);
                campaignBattleCard.SetAmount(battleCardHelper.GetCardComboAmount(turn.combos, campaignBattleCard.GetBattleCard().battleCardData));
                campaignBattleCard.TriggerFlipWithScaleAnimation();
                OnCardFinishedSummon(campaignBattleCard);
                break;
            }
        }
    }



    public void RestartGame()
    {
        RestartBoard();
        //isGameStarted = false;

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
        for (int i = 0; i < cardsHolder.transform.childCount; i++)
        {
            Destroy(cardsHolder.transform.GetChild(i).gameObject);
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

    public int GetNextEmptyCardPlaceHolderIndex()
    {
        return numOfCardsOnDeck;
    }

    public void DestroyLastCard()
    {
        Destroy(cardsHolder.transform.GetChild(cardsHolder.transform.childCount - 1).gameObject);
    }

  
}
