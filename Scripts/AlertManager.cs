using AppState.Player;
using Komino.GameEvents.Events;
using Komino.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState = null;
    [SerializeField] private PopupInfoEvent OnManaNeedToCollect = null;

    private PlayerAPI playerAPI = null;


    private void Awake()
    {
        playerAPI = new PlayerAPI();
    }

    public void CheckForManaCollection()
    {
        // Check if the 1 hour has passed since last coin collection
        if (playerState.LastManaCollection.Year > playerState.ServerTime.Year ||
              playerState.LastManaCollection.Month > playerState.ServerTime.Month ||
              playerState.LastManaCollection.Day > playerState.ServerTime.Day ||
            playerState.LastManaCollection.Hour - playerState.ServerTime.Hour > 1)
        {
            //Colllect new coins
            OnManaNeedToCollect.Raise(new PopupInfo("New Mana has recived Young Guru!", "Tap 'Collect' to gain new mana", "Collect", playerAPI.CollectMana));

            //Open timer for 1 hour to execute
            SetTimer(60);
            return;
        }

       // Set the timer for a seconds that remains until new mana can be collected
        SetTimer(60 - (Mathf.Abs(playerState.ServerTime.Hour - playerState.ServerTime.Hour) * 60 - Mathf.Abs(playerState.ServerTime.Minute - playerState.ServerTime.Minute)));

    }

    public void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        //Collect new Mana
        playerAPI.CollectMana();
        SetTimer(60);
    }

    public void SetTimer(int minutes)
    {
        var aTimer = new Timer(minutes * 60 * 1000); //one hour in milliseconds
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Start();
    }


}
