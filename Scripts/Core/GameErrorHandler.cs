using BestHTTP;
using Komino.GameEvents.Events;
using Komino.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Net;

namespace Komino.Core
{
    [CreateAssetMenu(fileName = "Error Handler", menuName = "Core/GameErrorHandler")]
    public class GameErrorHandler : ScriptableObject
    {
        [SerializeField] PopupInfoEvent onGameError = null;

        public void HandleError(GameError error, Func<Task> onGameErrorComplete)
        {

            switch (error)
            {
                case GameError.NOT_ENOUGH_MANA:

                    OnGameError("insufficent funds!","Please buy more mana to continue...","Got it",onGameErrorComplete);
                    break;
            }
        }

        private void OnGameError(string header,string message, string buttonText, Func<Task> callback)
        {
            onGameError.Raise(new PopupInfo(header, message, buttonText, callback));
        }
    }

}