using BestHTTP;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;
using Komino.CampaignBattle.Cards.BattleCards;
using Komino.Core;
using LitJson;
using AppState.Game;
using System.Net;
using Komino.CampaignBattle.Stats;
using Komino.GameEvents.Events;

public class Loader : MonoBehaviour
{
    [SerializeField] private Text infoText = null;
    [SerializeField] private HttpErrorHandler httpErrorHandler = null;
    [SerializeField] private CampaignBattleStats campaignBattleStats = null;


    [SerializeField] private GameState gameState = null;

    private CardsAPI cardsAPI = null;
    private BattleCardHelper battleCardHelper = null;
    private PvpAPI battleAPI = null;
    private ShopAPI shopAPI = null;

    private void Awake()
    {
        GameObject[] loaders = GameObject.FindGameObjectsWithTag("loader");

        if (loaders.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        battleAPI = new PvpAPI();
        cardsAPI = new CardsAPI();
        shopAPI = new ShopAPI();
        battleCardHelper = new BattleCardHelper();

        HTTPManager.MaxConnectionIdleTime = TimeSpan.FromSeconds(3);

    }

    public void PlayerLoggedIn()
    {
        Debug.Log("Player logged in.");
        EnterGame();

    }

    private async void EnterGame()
    {
        try
        {
            infoText.text = "Load cards from server...";
            // Load all cards data from the server - need to uncomment the next line
            
            List<CardData> cardsData = await cardsAPI.LoadAllBattleCardsWithPlayerCards();     
            gameState.Cards = battleCardHelper.CreateCardsDict(cardsData);

            infoText.text = "Load shop from server...";

            gameState.ShopData = await shopAPI.GetShop();

            print("Finished loads cards");

            LoadFirstSceneAccordingToBattleStatus();

        }
        catch (AsyncHTTPException e)
        {
            // TODO - think what callback to pass
            httpErrorHandler.HandleError(e, null);
        }
    }

    public async void LoadFirstSceneAccordingToBattleStatus()
    {
        if (GameMode.isInTestMode)
        {
            campaignBattleStats.SetCurrentBattleStatus(GameStatus.FIRST);
            LoadScene("FarmingCampaignBattle");
            return;
        }

        try
        {
            GameStatusData gameStatusData = await battleAPI.GetStatus();
            campaignBattleStats.SetCurrentBattleStatus(gameStatusData.game_status);

            if (gameStatusData.game_status == GameStatus.NEW)
            {
                LoadHomeScene();
                return;
            }

            if (gameStatusData.game_status == GameStatus.ONGOING)
            {
                LoadActiveBattleScene();
                return;
            }
        }

        catch (AsyncHTTPException e)
        {
            httpErrorHandler.HandleError(e, null);
            print(e);
        }
    }

    public void SetInfoText(string text)
    {
        infoText.text = text;
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public async void LoadNewBattleScene()
    {
        try
        {
            //Load the data for the next scene and Save it on champaignBattleStats
            gameState.BattleRoot = await battleAPI.GetNewBattle();

            Debug.Log("gameState.BattleRoot is " + gameState.BattleRoot);
            //Set max turns
            campaignBattleStats.SetMaxTurns((int)gameState.BattleRoot.max_turns);

            //Set battle status
            campaignBattleStats.SetCurrentBattleStatus(GameStatus.NEW);

            // Navigate to CampaignBattle Scene   
            LoadScene("FarmingCampaignBattle");
        }

        catch (AsyncHTTPException e)
        {
            httpErrorHandler.HandleError(e, null);
            print(e);
        }

    }

  

    public async void LoadActiveBattleScene()
    {
        try
        {
            //Load the data for the next scene and Save it on champaignBattleStats
            gameState.AllTurns = await battleAPI.GetActiveBattle();
            //Set battle status
            campaignBattleStats.SetCurrentBattleStatus(GameStatus.ONGOING);
            // Navigate to CampaignBattle Scene   
            LoadScene("FarmingCampaignBattle");
        }

        catch (AsyncHTTPException e)
        {
            httpErrorHandler.HandleError(e, null);
            print(e);
        }
    }

    public void LoadHomeScene()
    {
        //Load the data for the next scene and Save it on gameBattleStats
        //gameState.BattleRoot = await battleAPI.GetNewBattle();

        //Set battle status
        campaignBattleStats.SetCurrentBattleStatus(GameStatus.NEW);

        // Navigate to CampaignBattle Scene   
        LoadScene("Home");
    }

}
