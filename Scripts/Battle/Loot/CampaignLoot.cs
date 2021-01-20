using Komino.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignLoot : MonoBehaviour
{
    [SerializeField] ParticlesStorage particlesStorage = null;
    private Loot loot = null;
    private LootUI lootUI = null;


    private void Awake()
    {
        lootUI = GetComponent<LootUI>();
    }

    public void SetLoot(Loot loot)
    {
        this.loot = loot;
        // Update UI of cards after set the battle card
        lootUI.SetLootDisplay(loot);
    }

    public Loot GetLoot()
    {
        return this.loot;
    }
}
