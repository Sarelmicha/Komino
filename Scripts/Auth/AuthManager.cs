using System;
using System.Threading.Tasks;
using UnityEngine;
using BestHTTP;
using AppState.Player;
using ServerResponses.Player;
using Komino.GameEvents.Events;
using Komino.Core;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AppState.Game;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private VoidEvent onReAuthenticate = null;
    [SerializeField] private  HttpErrorHandler errorHandler = null;
    [SerializeField] private GPGSAuthentication GPGSAuthentication = null;
    [SerializeField] private CrashlyticsInit crashlyticsInit = null;


    public PlayerState playerState;
    private AuthAPI authAPI;
    private PlayerAPI playerAPI;
    public BundleWebLoader loader;
 

  


    private void Awake()
    {
        authAPI = new AuthAPI();
        playerAPI = new PlayerAPI();

    }

    public void Start()
    {
        Authenticate();
    }

    public void ReAuthenticate()
    {
        onReAuthenticate.Raise();
        Authenticate();
    }

    private async void Authenticate()
    {
        print("Connect was called.");

        try
        {
            crashlyticsInit.Init();
            GPGSAuthentication.Authenticate();
            Debug.Log("Try to login with Device id");
            PlayerInfoResponse playerInfoResponse = await authAPI.LoginWithDeviceId();
            LoadBundles();
            StartCoroutine(SetStateFromJsonResponse(playerInfoResponse));
        }

        catch (AsyncHTTPException e)
        {
            if (e.StatusCode == 422)
            {     
                try
                {
                    print("No user found, need to register");
                    await playerAPI.RegisterWithDeviceId();
                    PlayerInfoResponse playerInfoResponse = await authAPI.LoginWithDeviceId();
                    playerState.SetStateFromJsonResponse(playerInfoResponse);
                }

                catch (AsyncHTTPException e1)
                {
                    print("there has been an error with the registeration");
                    // There has been an error in registeration
                    errorHandler.HandleError(e1, ReAuthenticate);

                }
                return;
            }
            print("there has been an error with the login");
            // There has been an error in the login
            errorHandler.HandleError(e, ReAuthenticate);
        }
    }

    private void LoadBundles()
    {
        if (PlayerPrefs.GetInt("isBundleLoaded", 0) == 0)
        {
            loader.GetAllBundles();
        }
    }

    private IEnumerator SetStateFromJsonResponse(PlayerInfoResponse playerInfoResponse)
    {
        print("im here before and isFinishLoadBundles = " + PlayerPrefs.GetInt("isBundleLoaded"));
        yield return new WaitUntil(() => PlayerPrefs.GetInt("isBundleLoaded") == 1);
        print("im here after and isFinishLoadBundles = " + PlayerPrefs.GetInt("isBundleLoaded"));
        playerState.SetStateFromJsonResponse(playerInfoResponse);
    }
}
