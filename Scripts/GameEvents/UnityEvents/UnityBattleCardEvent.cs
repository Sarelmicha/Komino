using Komino.CampaignBattle.Cards.BattleCards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Komino.GameEvents.UnityEvents
{
    [System.Serializable] public class UnityBattleCardEvent : UnityEvent<BattleCard> { }
}
