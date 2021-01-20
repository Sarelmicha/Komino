using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrizeCard 
{
   
    public OpenPrizeData prizeData;
    public Sprite prizeCardSprite;

    public PrizeCard(OpenPrizeData prizeData, string name,string nation)
    {
        this.prizeData = prizeData;

        SetPrizeSprite(prizeData.type, name, nation);
    }

    private void SetPrizeSprite(string type,string name, string nation)
    {
        Debug.Log("type is " + type);

        if (type == "card")
        {
            try
            {
                this.prizeCardSprite = Resources.Load<Sprite>("Characters/" + nation + "/" + name + "/" + name + "1");
            }

            catch (FileNotFoundException e)
            {
                this.prizeCardSprite = null;
            }
            return;
        }

        this.prizeCardSprite = Resources.Load<Sprite>("Shop/Prizes/Type/" + type);
    }
}
