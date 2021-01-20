using AppState.Game;
using AppState.Player;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> sections = null;
    [SerializeField] private GameState gameState = null;
    [SerializeField] private PlayerState playerState = null;
    [SerializeField] private ShopCardLogic shopCardPrefab = null;
    [SerializeField] private PrizeCardLogic prizeCardLogicPrefab = null;

    [SerializeField] private GridLayoutGroup allChestsCards = null;
    [SerializeField] private GridLayoutGroup allCoinsCards = null;
    [SerializeField] private GridLayoutGroup allGemsCards = null;

    [SerializeField] private GameObject ShopArea = null;
    [SerializeField] private GameObject prizesArea = null;

    [SerializeField] private VoidEvent OnChestsHasBeenRefreshed = null;
    [SerializeField] private VoidEvent OnChestsHasBeenPurchased = null;
    [SerializeField] private VoidEvent onPrizesCanvasActive = null;
    [SerializeField] private VoidEvent onShopCanvasActive = null;

    PlayerStateManager playerStateManager = null;

    private List<ShopCard> coins = null;
    private List<ShopCard> gems = null;
    private List<ShopCard> chests = null;

    private ShopHelper shopHelper = null;
    private ShopAPI shopAPI = null;
    private float padding = 10f;

    private void Awake()
    {
        shopHelper = new ShopHelper();
        shopAPI = new ShopAPI();
        playerStateManager = GameObject.FindGameObjectWithTag("playerStateManager").GetComponent<PlayerStateManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpShop();
    }

    private void SetUpShop()
    {

        foreach (ShopData shopData in gameState.ShopData)
        {

            if (shopData.type == CurrenciesConstants.PLAYER_CHESTS)
            {
                chests = shopHelper.CreateShopCards(shopData.chests, shopData.type);
                InstantiateShopCards(chests, allChestsCards.transform);
            }

            else if (shopData.type == CurrenciesConstants.COIN)
            {
                coins = shopHelper.CreateShopCards(shopData.coins, shopData.type);
                InstantiateShopCards(coins, allCoinsCards.transform);
            }

            else if (shopData.type == CurrenciesConstants.GEM)
            {
                gems = shopHelper.CreateShopCards(shopData.gems, shopData.type);
                InstantiateShopCards(gems, allGemsCards.transform);
            }
        }
    }


    private void InstantiateShopCards(List<ShopCard> shopCards, Transform parent)
    {
        foreach (ShopCard shopCard in shopCards)
        {
            ShopCardLogic newShopCard = Instantiate(shopCardPrefab, parent.position, Quaternion.identity);
            newShopCard.SetShopCard(shopCard);
            newShopCard.transform.SetParent(parent.transform);
        }
    }

    private void InstantiatePrizesCards(List<PrizeCard> prizes, Transform parent)
    {
        foreach (PrizeCard prize in prizes)
        {
            PrizeCardLogic newPrizeCard = Instantiate(prizeCardLogicPrefab, parent.position, Quaternion.identity);
            newPrizeCard.SetPrizeCard(prize);
            newPrizeCard.transform.SetParent(parent.transform);
        }
    }

    public async void PurchaseChest(ShopCardLogic shopCardLogic)
    {

        if (shopCardLogic.GetShopCard().shopCardData.buy_with_gems)
        {
            if (playerState.Gems < shopCardLogic.GetShopCard().shopCardData.cost)
            {
                Debug.Log("Not enough gems.");
                return;
            }
        }

        if (!shopCardLogic.GetShopCard().shopCardData.buy_with_gems)
        {
            if (playerState.Coins < shopCardLogic.GetShopCard().shopCardData.cost)
            {
                Debug.Log("playerState.Coins = " + playerState.Coins + " and shopCardLogic.GetShopCard().shopCardData.cost =  " + shopCardLogic.GetShopCard().shopCardData.cost); ;
                Debug.Log("Not enough coins.");
                return;
            }
        }
     
        // There is enough currency for purchased
        Debug.Log("Lets the pruchase begin.");
        List<OpenPrizeData> prizes = await shopAPI.PurchaseChest(shopCardLogic.GetShopCard().shopCardData.id.ToString());
        print("OnChestsHasBeenPurchased has been raised!");
        OnChestsHasBeenPurchased.Raise();
        List<PrizeCard> allPrizes = shopHelper.CreatePrizesCards(prizes,gameState.Cards);

        InstantiatePrizesCards(allPrizes, prizesArea.GetComponentInChildren<GridLayoutGroup>().transform);
        ShowPrizesCanvas();

        // Make the purchased chest unclickable
        shopCardLogic.SetInteractable(false);

        //Reduce the amount
        ReduceCost(shopCardLogic.GetShopCard().shopCardData.buy_with_gems, shopCardLogic.GetShopCard().shopCardData.cost);
    }

    private void ReduceCost(bool buyWithGems, int cost)
    {
        if (buyWithGems)
        {
            playerStateManager.SetGems(new Value((int)playerStateManager.GetGems() - cost));
        }

        else
        {
            playerStateManager.SetCoins(new Value((int)playerStateManager.GetCoins() - cost));
        }
    }

    public void ShowPrizesCanvas()
    {
        prizesArea.SetActive(true);
        ShopArea.SetActive(false);
        onPrizesCanvasActive.Raise();
    }

    public void ShowShopCanvas()
    {
        prizesArea.SetActive(false);
        ShopArea.SetActive(true);
        onShopCanvasActive.Raise();

        DestoryAllGridChildren(prizesArea.GetComponentInChildren<GridLayoutGroup>());
    }

    public void DestoryAllGridChildren(GridLayoutGroup grid)
    {
     
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
    }

    public async void RefreshPrizes()
    {
        int numOfGemsToRefresh = gameState.GetChestsData().Item2;
        if (playerState.Gems < numOfGemsToRefresh)
        {
            Debug.Log("Not enugh gems to refresh chests");
            return;
        }

        ShopDataRoot shopRoot = await shopAPI.RefreshChests();

        foreach (ShopData shopData in gameState.ShopData)
        {
            if (shopData.type == CurrenciesConstants.PLAYER_CHESTS)
            {
                shopData.chests = shopRoot.shop.chests;

                shopData.seconds_left = shopRoot.shop.seconds_left;
                shopData.refresh_gem_price = shopRoot.shop.refresh_gem_price;
                shopData.refresh_gem_round_in_minutes = shopRoot.shop.refresh_gem_round_in_minutes;

                DestoryAllGridChildren(allChestsCards);


                chests = shopHelper.CreateShopCards(shopData.chests, shopData.type);
                InstantiateShopCards(chests, allChestsCards.transform);
            }
        }

        ReduceCost(true, numOfGemsToRefresh);

        print("OnChestsHasBeenRefreshed has been raised!");
        OnChestsHasBeenRefreshed.Raise();
    }
   
}
