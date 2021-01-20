using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.GameEvents.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}