﻿using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Komino.GameEvents.UnityEvents
{
    [System.Serializable] public class UnityShopCardEvent : UnityEvent<ShopCardLogic> { }
}
