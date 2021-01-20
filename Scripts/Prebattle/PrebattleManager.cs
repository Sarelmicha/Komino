//using AppState.Game;
//using AppState.Player;
//using BestHTTP;
//using Komino.CampaignBattle.Cards.BattleCards;
//using Komino.CampaignBattle.Stats;
//using Komino.Core;
//using Komino.Enums;
//using Komino.GameEvents.Events;
//using ServerResponses.Player;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class PrebattleManager : MonoBehaviour
//{

//    [SerializeField] GridLayoutGroup playerGrid = null;
//    [SerializeField] GridLayoutGroup enemyGrid = null;
//    [SerializeField] GameState gameState = null;
//    [SerializeField] CampaignBattleStats campaignBattleStats = null;
//    [SerializeField] DeckBattleCard deckBattleCardPrefab = null;
//    [SerializeField] HttpErrorHandler httpErrorHandler = null;

//    [SerializeField] IntEvent onPlayerDeckPowerUpdated = null;
//    [SerializeField] IntEvent onEnemyDeckPowerUpdated = null;

//    [SerializeField] GameErrorHandler gameErrorHandler = null;
//    [SerializeField] PlayerState playerState = null;
//    [SerializeField] GameObject spinner = null;


//    private IBattleAPI battleAPI = null;
//    private float gridScale = 0.8f;
//    private bool isPlayerChooseBattleCost = false;
//    private BattleRoot preBattleRoot = null;
//    private Loader loader = null;


//    private void Awake()
//    {
//        loader = GameObject.FindGameObjectWithTag("loader").GetComponent<Loader>();
//    }

//    public void SetBattleAPI(IBattleAPI battleAPI)
//    {
//        this.battleAPI = battleAPI;
//    }

//    private void Start()
//    {
//        SetUpDeck();
//    }

//    private void SetUpDeck()
//    {
    
//        try
//        {
//            preBattleRoot = gameState.PreBattleRoot;
//            print("onPlayerDeckPowerUpdated has been raised");
//            onPlayerDeckPowerUpdated.Raise(preBattleRoot.player_deck_power);
//            print("onEnemyDeckPowerUpdated has been raised");
//            onEnemyDeckPowerUpdated.Raise(preBattleRoot.bot_deck_power);


//            for (int i = 0; i < preBattleRoot.player_cards.Count; i++)
//            {
//                CardData cardData = gameState.GetCard(preBattleRoot.player_cards[i]._id);
//                cardData.cost = preBattleRoot.player_cards[i].cost;
//                InstantiatePreGameCard(new BattleCard(cardData), playerGrid,PlayerType.Player);
//            }

//            // Summon Enemys cards
//            for (int i = 0; i < preBattleRoot.bot_cards.Count; i++)
//            {
//                CardData cardData = gameState.GetCard(preBattleRoot.bot_cards[i]);
//                //Set cost to 0
//                cardData.cost = 0;
//                cardData.loot = 0;
//                InstantiatePreGameCard(new BattleCard(cardData), enemyGrid,PlayerType.Enemy);
//            }   

//            SetGridScale(playerGrid, gridScale);
//            SetGridScale(enemyGrid, gridScale);

//        }

//        catch (AsyncHTTPException e)
//        {
//            httpErrorHandler.HandleError(e, null);
//        }
//    }

//    public async void onOpenCardPicked(BattleCard pickedCard)
//    {

//        if (!isEnoughMana(pickedCard.battleCardData.cost))
//        {
//            gameErrorHandler.HandleError(GameError.NOT_ENOUGH_MANA, null);
//            return;
//        }
//        spinner.SetActive(true);
//        playerState.Mana -= pickedCard.battleCardData.cost;
//        await loader.LoadNewBattleScene(pickedCard.battleCardData._id);
//        return;
//    }

//    private bool isEnoughMana(int manaCost)
//    {
//        if (playerState.Mana - manaCost >= 0)
//        {
//            return true;
//        }

//        return false;
//    }

//    private void SetGridScale(GridLayoutGroup grid, float size)
//    {
//        grid.transform.localScale = new Vector3(size, size, size);
//    }

//    private void InstantiatePreGameCard(BattleCard battleCard, GridLayoutGroup grid,PlayerType playerType)
//    {
//        DeckBattleCard deckBattleCard = Instantiate(deckBattleCardPrefab, grid.transform.position, Quaternion.identity);
//        deckBattleCard.SetBattleCard(battleCard);
//        deckBattleCard.transform.SetParent(grid.transform);
//        if (playerType == PlayerType.Enemy)
//        {
//            deckBattleCard.GetComponent<Button>().enabled = false;
//        }
//    }
//}
