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
    [CreateAssetMenu(fileName = "Error Handler", menuName = "Core/HttpErrorHandler")]
    public class HttpErrorHandler : ScriptableObject
    {
        [SerializeField] PopupInfoEvent onHttpError = null;

        public async void HandleError(AsyncHTTPException error, Func<Task> onHttpErrorComplete)
        {
            switch (error.StatusCode)
            {

                case (int)HttpStatusCode.RequestTimeout:
                    //Time Out                 
                    Debug.LogError(error);
                    RaisePopupError("Requested Time Out",error.Message, "Retry", onHttpErrorComplete);
                    break;
                case (int)HttpStatusCode.GatewayTimeout:
                    Debug.LogError(error);
                    RaisePopupError("GatewayTimeOut",error.Message, "Retry", onHttpErrorComplete);
                    break;
                case 422:
                    //Unprocessable Entity
                    //TODO- check what code in the error and follow accordinly
                   //await GameObject.FindGameObjectWithTag("loader").GetComponent<Loader>().LoadBattleSceneAccordingToBattleStatus(null);
                   break;

                case 0:
                    //Connected time out
                    Debug.LogError(error);
                    RaisePopupError("Connected Time Out!",error.Message, "Retry", onHttpErrorComplete);
                    break;
                default:
                    Debug.LogError(error);
                    break;
            }
        }

        public void HandleError(AsyncHTTPException error, Action<bool> onHttpErrorComplete,bool value)
        {

            switch (error.StatusCode)
            {
                case 422:
                    // Unprocessable Entity
                    Debug.LogError(error);
                    onHttpErrorComplete.Invoke(value);
                    break;
                case (int)HttpStatusCode.Gone:
                    Debug.LogError(error);
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    //Internal Error
                    Debug.LogError(error);
                    onHttpErrorComplete.Invoke(value);

                    break;
                default:
                    Debug.LogError(error);
                   

                    break;
            }
        }

        public void HandleError(AsyncHTTPException error, Action onHttpErrorComplete)
        {

            switch (error.StatusCode)
            {
                case 422:
                    // Unprocessable Entity
                    Debug.LogError(error);
                    break;
                case (int)HttpStatusCode.Gone:
                    Debug.LogError(error);
                    onHttpErrorComplete.Invoke();
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    //Internal Error
                    Debug.LogError(error);
                    onHttpErrorComplete.Invoke();
                    break;
                default:
                    Debug.LogError(error);
                    
                    break;
            }
        }

        private void RaisePopupError(string errorHeader,string errorMessage,  string buttonText, Func<Task> callback)
        {
            onHttpError.Raise(new PopupInfo(errorHeader, errorMessage, buttonText, callback));
        }
    }

}
