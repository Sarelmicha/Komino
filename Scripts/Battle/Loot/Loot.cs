using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Sprite lootSprite;
    public LootData lootData;
    public bool isReached = false;

    public Loot(LootData lootData, bool isReached)
    {
        this.lootData = lootData;
        IsReached(isReached);

        if (lootData.type != null)
        {
            SetLootSprite(lootData);
        }
    }

    private void SetLootSprite(LootData lootData)
    {
        if (lootData.type == LootConstants.LOOT)
        {
            if (isReached)
            {
                lootSprite = Resources.Load<Sprite>("Loot/Loot/Filled/" + lootData.type);
            }

            else
            {
                lootSprite = Resources.Load<Sprite>("Loot/Loot/Empty/" + lootData.type);
            }

            return;
        }

        if (lootData.type == LootConstants.SKILL)
        {

            if (isReached)
            {
                lootSprite = Resources.Load<Sprite>("Loot/Skill/Filled/" + lootData.skill);
            }

            else
            {
                lootSprite = Resources.Load<Sprite>("Loot/Skill/Empty/" + lootData.skill);
            }
        }


        
    }

    public void IsReached(bool isReached)
    {
        this.isReached = isReached;
    }

    public bool IsReached()
    {
        return this.isReached;
    }
}
