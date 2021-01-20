using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerAPI
{

    private string PLAYER = "player";

    public Task<string> RegisterWithDeviceId()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl(PLAYER + "/mobile/register")), HTTPMethods.Post);
        request.AddField("device_login[device_id]", SystemInfo.deviceUniqueIdentifier);
        return request.GetAsStringAsync();
    }

    public Task<int> CollectMana()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl(PLAYER + "/collect-mana")), HTTPMethods.Patch);
        return request.GetFromJsonResultAsync<int>();
    }

    public Task<PlayerCurrenciesData> UpgradeCard(string id)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl(PLAYER + "/cards/upgrade/" + id)), HTTPMethods.Patch);
        return request.GetFromJsonResultAsync<PlayerCurrenciesData>();
        
    }


    public Task<PlayerCurrenciesData> GetCurrencies()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl(PLAYER + "/currencies")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<PlayerCurrenciesData>();

    }

    public Task<AutomateBattleSettings> SetAutomationBattleSettings(string type)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl(PLAYER + "/automate-battle")), HTTPMethods.Patch);

        string json = "{ \"type\": " + " \"" + type + "\"}";
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = Encoding.UTF8.GetBytes(json);
        return request.GetFromJsonResultAsync<AutomateBattleSettings>();
    }
}
