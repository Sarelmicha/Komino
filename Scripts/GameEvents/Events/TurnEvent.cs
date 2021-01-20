using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.GameEvents.Events
{
    [CreateAssetMenu(fileName = "New Turn Event", menuName = "Game Events/Turn")]
    public class TurnEvent : BaseGameEvent<Turn>
    {

    }
}
