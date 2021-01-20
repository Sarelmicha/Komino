
using Komino.GameEvents.Events;
using Komino.GameEvents.UnityEvents;
using ServerResponses.Player;
using UnityEngine;

namespace Komino.GameEvents.Listeners
{
    public class DamageListener : BaseGameEventListener<Damage, DamageEvent, UnityDamageEvent>
    {

    }
}