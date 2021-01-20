using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootUI : MonoBehaviour
{
    [SerializeField] private Image lootImage = null;

    public void SetLootDisplay(Loot loot)
    {
        if (loot.lootSprite != null)
        {
            lootImage.sprite = loot.lootSprite;
        }
    }
}
