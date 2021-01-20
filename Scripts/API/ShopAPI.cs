using BestHTTP;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShopAPI 
{
    public Task<List<ShopData>> GetShop()
    {
        Debug.Log("inside getShop");
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("shop")), HTTPMethods.Get);
        return request.GetFromJsonResultAsync<List<ShopData>>();
    }

    public Task<List<OpenPrizeData>> PurchaseChest(string id)
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("shop/player-chest/" + id)), HTTPMethods.Patch);
        return request.GetFromJsonResultAsync<List<OpenPrizeData>>();
    }

    public Task<ShopDataRoot> RefreshChests()
    {
        HTTPRequest request = new HTTPRequest(new Uri(Config.GetServerUrl("shop/refresh-player-chests/")), HTTPMethods.Patch);
        return request.GetFromJsonResultAsync<ShopDataRoot>();
    }
}
