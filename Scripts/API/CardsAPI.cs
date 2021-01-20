using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardsAPI
{
    public  Task<List<CardData>> LoadAllBattleCards()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("cards/info")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<List<CardData>>();
    }

    public Task<List<CardData>> LoadAllBattleCardsWithPlayerCards()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("cards/cards-with-player-cards")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<List<CardData>>();
    }
}
