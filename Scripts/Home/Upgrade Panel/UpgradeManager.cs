using AppState.Game;
using BestHTTP;
using Komino.CampaignBattle.Cards.BattleCards;
using Komino.Core;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{


    [SerializeField] GridLayoutGroup playerCardsGrid = null;
    [SerializeField] GridLayoutGroup toBeFoundCardsGrid = null;

    [SerializeField] DeckBattleCard deckBattleCardPrefab = null;
    [SerializeField] GameState gameState = null;
    [SerializeField] CardInfo cardInfo = null;
    [SerializeField] HttpErrorHandler httpErrorHandler = null;
    [SerializeField] UpgradeHUDManager upgradeHUDManager = null;

    private List<BattleCard> allCards = null;
    private PlayerStateManager playerStateManager = null;
    private BattleCardHelper battleCardHelper = null;
    private BattleCard pickedCard = null;
    private PlayerAPI playerAPI = null;
    private CardsAPI cardsAPI = null;

    void Awake()
    {
        battleCardHelper = new BattleCardHelper();
        playerAPI = new PlayerAPI();
        cardsAPI = new CardsAPI();
        playerStateManager = GameObject.FindGameObjectWithTag("playerStateManager").GetComponent<PlayerStateManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cardInfo.Hide();
        SetUpDeck();
        SetUpPlayerStats();    
    }

 
    private void SetUpPlayerStats()
    {
        Debug.Log("coins = " + playerStateManager.GetCoins());
        playerStateManager.SetCoins(new Value((int)playerStateManager.GetCoins(), true));
        playerStateManager.SetGems(new Value((int)playerStateManager.GetGems(), true));


    }

    private void SetUpDeck()
    {
        allCards = battleCardHelper.CreateBattleCards(gameState.Cards);

        //allCards.Sort();

        int numOfOwnedCards = 0;

        foreach (BattleCard battleCard in allCards)
        {

            DeckBattleCard newDeckBattleCard = Instantiate(deckBattleCardPrefab, playerCardsGrid.transform.position, Quaternion.identity);
            newDeckBattleCard.SetBattleCard(battleCard);
            newDeckBattleCard.transform.SetParent(playerCardsGrid.transform);

            if (!battleCard.battleCardData.player_card.owned)
            {
                newDeckBattleCard.ShowAsUnowned();
            }

            else
            {
                numOfOwnedCards++;
            }
        }

        upgradeHUDManager.UpdateNumOfOwnedCards(numOfOwnedCards, allCards.Count);
    }


    public void onDeckCardClicked(BattleCard pickedCard)
    {
        //Set the current card to be the picked card
        this.pickedCard = pickedCard;

        //Check if the player has already have this card or not
        if (pickedCard.battleCardData.player_card.owned && pickedCard.battleCardData.levels[0] != null)
        {

            print("num of player card sculputers " + pickedCard.battleCardData.player_card.sculptures);
            print("num of picked card sculputers " + pickedCard.battleCardData.player_card.sculptures);

            print("num of player coins " + playerStateManager.GetCoins());
            print("num of card coins " + pickedCard.battleCardData.levels[1].cost.coins);

            if ((pickedCard.battleCardData.player_card.sculptures >= pickedCard.battleCardData.levels[1].cost.sculptures)
                && (playerStateManager.GetCoins() >= pickedCard.battleCardData.levels[1].cost.coins))
            {
                //if it does check if the player have enugh curriences to upgrade
                cardInfo.Show(pickedCard, true, true);
            }
            else
            {
                cardInfo.Show(pickedCard, true, false);
            }

            return;
        }

        cardInfo.Show(pickedCard, false, false);
    }

    public async void UpgradeCard()
    {
        try
        {
            print("card id is " + pickedCard.battleCardData._id);
            print("before upgrade card");
            PlayerCurrenciesData currentPlayerCurrencies = await playerAPI.UpgradeCard(pickedCard.battleCardData._id);
            print("player coins is " + currentPlayerCurrencies.coins);
            print("player coins is " + currentPlayerCurrencies.gems);
            playerStateManager.SetCoins(new Value((int)currentPlayerCurrencies.coins, false));
            UpdateDeck();         
        }
        catch (AsyncHTTPException e)
        {
            Debug.LogError(e);
            httpErrorHandler.HandleError(e, null);
        }
    }

    public async void UpdateDeck()
    {
        List<CardData> cardsData = await cardsAPI.LoadAllBattleCardsWithPlayerCards();
        gameState.Cards = battleCardHelper.CreateCardsDict(cardsData);

        DestoryDeck();
        SetUpDeck();
        SetUpPlayerStats();
    }

    private void DestoryDeck()
    {
        for (int i = 0; i < playerCardsGrid.transform.childCount; i++)
        {
            Destroy(playerCardsGrid.transform.GetChild(i).gameObject);
        }
    }
}
