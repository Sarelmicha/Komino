using AppState.Game;
using AppState.Player;
using Komino.CampaignBattle;
using Komino.CampaignBattle.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleGameManager : MonoBehaviour
{
    private PvpAPI pvpAPI = null;
    private TutorialAPI tutorialAPI = null;
    private TestAPI testAPI = null;

    [SerializeField] private BattleManager battleManager = null;
    [SerializeField] private TutorialManager tutorialManager = null;
    [SerializeField] private CampaignBattleStats campaignBattleStats = null;
    [SerializeField] private GameState gameState = null;
    //In the future tutorial manager will be here too.


    void Awake()
    {
        pvpAPI = new PvpAPI();
        tutorialAPI = new TutorialAPI();
        testAPI = new TestAPI();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Check if the player is in a tutorial or not.

        //if it does handle tutorial

        if (GameMode.isInTutorialMode)
        {
            tutorialManager.HandleTutorial();
            //......
            return;
        }

        //else if (GameMode.isInTestMode)
        //{
        //    battleManager.SetBattleAPI(testAPI);

        //    if (campaignBattleStats.GetCurrentBattleStatus() == "first")
        //    {
        //        battleManager.InitalizeBattleTestMode();
        //        return;
        //    }

        //    if (campaignBattleStats.GetCurrentBattleStatus() == GameStatus.ONGOING)
        //    {
        //        battleManager.SetActiveBattle(gameState.AllTurns);
        //        return;
        //    }
        //}

        else if (GameMode.inProductionMode)
        {
            battleManager.SetBattleAPI(pvpAPI);

            if (campaignBattleStats.GetCurrentBattleStatus() == GameStatus.NEW)
            {
                battleManager.GetFirstBattleCard(gameState.BattleRoot);
                return;
            }


            if (campaignBattleStats.GetCurrentBattleStatus() == GameStatus.ONGOING)
            {
                battleManager.SetActiveBattle(gameState.AllTurns);
                return;
            }
        }   
    }
}
