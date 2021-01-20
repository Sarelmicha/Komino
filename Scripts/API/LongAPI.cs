using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LongAPI
{
    public Task<BattleCampaignRoot> GetFirstTurns()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/long")), HTTPMethods.Post);
        return request.GetFromJsonResultAsync<BattleCampaignRoot>();
    }

    public Task<LongTurnRoot> GetNextTurn()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/long/turn")), HTTPMethods.Post);

        return request.GetFromJsonResultAsync<LongTurnRoot>();
    }

    public Task<TurnsRoot> Restart(string type)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("battle/long/restart/" + type)), HTTPMethods.Post);


        return request.GetFromJsonResultAsync<TurnsRoot>();

    }


}
