using Komino.Core;
using ServerResponses.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Komino.CampaignBattle.Cards.BattleCards
{
    public class BattleCard : IComparable<BattleCard>
    {
        public CardData battleCardData;

        public Sprite facePanelSprite;
        public Sprite backPanelSprite;
        public Sprite creatureSprite;
        public Sprite abilitySprtie;
        public ParticlesStorage particlesStorage;
        public PersistenceDataManager persistenceDataManager;

        public BattleCard(CardData cardData)
        {
            
            this.battleCardData = cardData;

            if (cardData == null)
            {
                return;
            }

            if (cardData.nation != null && cardData.display_name != null)
            {
                SetCreature(cardData.nation, cardData.identifier_name);
            }

            if (cardData.tier != null)
            {
                SetFacePanel(cardData.tier);
            }

            if (cardData.abilities != null && cardData.abilities.Count > 0)
            {
                SetAbility(cardData.abilities[0]);
            }
        }

        private void SetAbility(string ability)
        {
            abilitySprtie = Resources.Load<Sprite>("Abilities/" + ability);
        }

        private void SetFacePanel(string tier)
        {
            facePanelSprite = Resources.Load<Sprite>("Characters/Tier/" + tier);
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


        public override string ToString()
        {
            return "id " + battleCardData._id + "\ndisplay name " + battleCardData.display_name + "\nnation " + battleCardData.nation + "\nteir " + battleCardData.tier +
                "\nlevel " + battleCardData.level + "\nattack " + battleCardData.attack + "\npower " + battleCardData.power + "\nabilities " + battleCardData.abilities +
                "\ncritical hit chance " + battleCardData.critical_hit_chance + "\ncritical hit damage " + battleCardData.critical_hit_damage + "\nchance to appear in desk " +
                battleCardData.chance_to_appear_in_deck + "\nluck " + battleCardData.loot;
        }

        public int CompareTo(BattleCard other)
        {
            if (this.battleCardData.level > other.battleCardData.level)
                return 1;
            else if (this.battleCardData.level < other.battleCardData.level)
                return -1;
            return 0;
        }
    }
}
