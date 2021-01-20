using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHelper
{
    public List<ShopCard> CreateShopCards(List<ShopCardData> curriences, string currencyType)
    {
        List<ShopCard> shopCards = new List<ShopCard>();

        foreach (ShopCardData shopCardData in curriences)
        {
            Debug.Log("shopCardData is " + shopCardData);
            shopCards.Add(new ShopCard(shopCardData, currencyType));
        }

        return shopCards;
    }

    public List<PrizeCard> CreatePrizesCards(List<OpenPrizeData> prizes,Dictionary<string,CardData> allCards)
    {
        List<PrizeCard> allPrizes = new List<PrizeCard>();

        foreach (OpenPrizeData prizeData in prizes)
        {
            string name = null;
            Debug.Log("prizeData is " + prizeData);
            if (prizeData.card != null)
            {
               name = prizeData.card[0];
            }     
            allPrizes.Add(new PrizeCard(prizeData, name == null? null :allCards[name].identifier_name, name == null? null : allCards[name].nation));
        }

        return allPrizes;
    }
}
