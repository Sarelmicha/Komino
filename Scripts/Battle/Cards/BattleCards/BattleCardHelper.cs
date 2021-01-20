using Komino.CampaignBattle.Cards.BattleCards;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardHelper
{

    public List<BattleCard> CreateBattleCards(List<CardData> cards)
    {
        List<BattleCard> battleCards = new List<BattleCard>();

        foreach (CardData cardData in cards)
        {
            battleCards.Add(new BattleCard(cardData));
        }

        return battleCards;
    }


  

    public List<BattleCard> CreateBattleCards(List<Turn> turns)
    {
        List<BattleCard> battleCards = new List<BattleCard>();

        foreach (Turn turn in turns)
        {
            if (turn == null)
            {
                battleCards.Add(new BattleCard(null));
            }

            else
            {
                battleCards.Add(new BattleCard(turn.card));
            }
       
        }

        return battleCards;
    }

    /*public List<BattleCard> CreateBattleCards(List<PartialDataTurn> turns)
    {
        List<BattleCard> battleCards = new List<BattleCard>();

        foreach (PartialDataTurn turn in turns)
        {
            Debug.Log("im here in enemy turn! and my nation is " + turn.nation);
            battleCards.Add(new BattleCard(new CardData(turn.nation)));
        }

        return battleCards;
    }
    */

    public BattleCard CreateBattleCard(CardData card)
    {
        return new BattleCard(card);
    }

    public int CountSameCards(List<Combo> combos)
    {
        int numOfSameCards = 0;

        for (int i = 0; i < combos.Count; i++)
        {
            if (combos[i].amount > 1)
            {
                numOfSameCards += (int)combos[i].amount - 1;
            }
        }

        return numOfSameCards;
    }

    public double GetCardComboAmount(List<Combo> combos, CardData cardData)
    {
        if (combos == null)
        {
            return 0;
        }
        foreach (Combo combo in combos)
        {
          
            if (combo.card._id == cardData._id)
            {
                Debug.Log("combo amount is " + combo.amount);
                return combo.amount;

            }
        }
        return 0;
    }

    public Dictionary<string, CardData> CreateCardsDict(List<CardData> cardsData)
    {
        Dictionary<string, CardData> allCards = new Dictionary<string, CardData>();

        foreach (CardData cardData in cardsData)
        {
            allCards.Add(cardData._id, cardData);
        }

        return allCards;
    }

    public List<BattleCard> CreateBattleCards(Dictionary<string,CardData> cards)
    {
        List<BattleCard> battleCards = new List<BattleCard>();


        foreach (KeyValuePair<string, CardData> entry in cards)
        {
            battleCards.Add(new BattleCard(entry.Value));
        }

        return battleCards;
    }

    public List<PrizeCard> CreatePrizes(BattlePrizeData prize)
    {
        List<PrizeCard> allPrizes = new List<PrizeCard>();

        if (prize.currencies.coins != 0)
        {
            OpenPrizeData coinsData = new OpenPrizeData();
            coinsData.type = "coins";
            coinsData.value = prize.currencies.coins;
            allPrizes.Add(new PrizeCard(coinsData, null, null));
        }

        if (prize.currencies.gems != 0)
        {
            OpenPrizeData gemsData = new OpenPrizeData();
            gemsData.type = "gems";
            gemsData.value = prize.currencies.coins;
            allPrizes.Add(new PrizeCard(gemsData, null, null));
        }

        return allPrizes;   
    }
}
