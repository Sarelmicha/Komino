using BestHTTP;
using ServerResponses.Player;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CampaignBattleAPI : IBattleAPI
{
    public Task<BattleRoot> GetNewBattle()
    {      
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign")), HTTPMethods.Post);
        return request.GetFromJsonResultAsync<BattleRoot>();
    }

    public Task<BattleRoot> GetActivePreBattle()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/first-turn")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<BattleRoot>();
    }

    public Task<TurnRoot> GetNextTurn(bool luckyStrike)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/turn")), HTTPMethods.Post);

        string luckyStrikeJson = luckyStrike ? "true" : "false";

        string json = "{ \"lucky_strike\": " + luckyStrikeJson + "}";
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(json);

        return request.GetFromJsonResultAsync<TurnRoot>();
    }

    public Task<BattleCampaignRoot> GetAllTurns()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/fight")), HTTPMethods.Post);
        return request.GetFromJsonResultAsync<BattleCampaignRoot>();
    }

    public Task<BattleCampaignRoot> GetActiveBattle()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/active-battle")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<BattleCampaignRoot>();
    }

    public Task<BattleCostRoot> GetBattleCost()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/cost")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<BattleCostRoot>();
    }

    public Task<GameStatusData> GetStatus()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/status")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<GameStatusData>();
    }


    public Task<BattleCampaignRoot> GetFirstTurn(bool luckyStart)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/campaign/first-turn")), HTTPMethods.Post);
        string luckyStartJson = luckyStart? "true" : "false";
        string json = "{ \"lucky_start\": " + luckyStartJson + "}";
        Debug.Log(json);
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(json);
        return request.GetFromJsonResultAsync<BattleCampaignRoot>();
    }

    public Task<TurnRoot> GetFirstTurn(string card_id)
    {
        throw new NotImplementedException();
    }

    public Task<TurnRoot> GetNextTurn(string choice)
    {
        throw new NotImplementedException();
    }

    public Task<BattleCampaignRoot> GetAllTurns(string choice)
    {
        throw new NotImplementedException();
    }

    Task<AllTurns> IBattleAPI.GetActiveBattle()
    {
        throw new NotImplementedException();
    }
}
