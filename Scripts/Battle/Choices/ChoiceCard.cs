using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceCard
{
    public CardData cardData;

    public Sprite backgroundSprite;
    public Sprite creatureSprite;

    public ChoiceCard(CardData cardData)
    {
        this.cardData = cardData;

        if (cardData.tier != null)
        {
            SetBackground(cardData.tier);
        }

        if (cardData.nation != null && cardData.display_name != null)
        {
            SetCreature(cardData.nation, cardData.display_name);
        }

    }

    public void SetCreature(string nation, string name)
    {
        //try
        //{
        //    this.creatureSprite = PersistenceDataManager.LoadImageFromDisk(name + "1").Item1;
        //}

        //catch (FileNotFoundException e)
        //{
        //    this.creatureSprite = null;
        //}


        this.creatureSprite = Resources.Load<Sprite>("Characters/" + nation + "/" + name + "/" + name + "1");
    }


    public Sprite GetCreature()
    {
        return this.creatureSprite;
    }

    private void SetBackground(string tier)
    {
        backgroundSprite = Resources.Load<Sprite>("Characters/Tier/" + tier);
    }
}
