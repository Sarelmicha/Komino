using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PvpAPI : IBattleAPI
{

    public Task<BattleRoot> GetNewBattle()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/pvp")), HTTPMethods.Post);
        return request.GetFromJsonResultAsync<BattleRoot>();
    }


    public Task<TurnRoot> GetNextTurn(string choice)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/pvp/turn")), HTTPMethods.Post);

        string json = "{ \"choice\": " + " \"" + choice + "\"}";
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(json);

        return request.GetFromJsonResultAsync<TurnRoot>();
    }

    public Task<BattleCampaignRoot> GetAllTurns(string choice)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/pvp/turn")), HTTPMethods.Post);

        string json = "{ \"choice\": " + " \"" + choice + "\"}";
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(json);

        return request.GetFromJsonResultAsync<BattleCampaignRoot>();
    }


    public Task<AllTurns> GetActiveBattle()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/pvp/active-battle")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<AllTurns>();
    }


    public Task<GameStatusData> GetStatus()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/pvp/status")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<GameStatusData>();
    }
 
}
