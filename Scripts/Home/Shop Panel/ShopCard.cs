using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCard
{
    public ShopCardData shopCardData;
    public Sprite shopCardSprite;
    public Sprite currenceySprite;


    public ShopCard(ShopCardData shopCardData, string type)
    {
        this.shopCardData = shopCardData;

        Debug.Log("shopCardData = " + this.shopCardData);

        if (type != null)
        {
            SetShopCardSprite(type, shopCardData.name);
        }

        SetCurrenecySprite();
    }

    private void SetShopCardSprite(string type, string name)
    {

        Debug.Log("type is " + type);
        if (type == CurrenciesConstants.PLAYER_CHESTS)
        {
            this.shopCardSprite = Resources.Load<Sprite>("Shop/Type/Chests/" + name);
            return;
        }

        this.shopCardSprite = Resources.Load<Sprite>("Shop/Type/" + type);
    }

    private void SetCurrenecySprite()
    {
        if (shopCardData.buy_with_gems)
        {
            this.currenceySprite = Resources.Load<Sprite>("Shop/Currencey/Type/" + "gems");
            return;
        }

        this.currenceySprite = Resources.Load<Sprite>("Shop/Currencey/Type/" + "coins");
    }
}
