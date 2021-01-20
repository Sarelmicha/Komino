using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerResponses.Player;
using UnityEngine;

public class TutorialAPI : IBattleAPI
{
  

    public Task<BattleCostRoot> GetBattleCost()
    {
        throw new System.NotImplementedException();
    }

    public Task<BattleCampaignRoot> GetFirstTurn(bool luckyStart)
    {
        throw new System.NotImplementedException();
    }

   

    public Task<AllTurns> GetActiveBattle()
    {
        throw new System.NotImplementedException();
    }

    public Task<BattleRoot> GetNewBattle()
    {
        throw new System.NotImplementedException();
    }

    public Task<GameStatusData> GetStatus()
    {
        throw new System.NotImplementedException();
    }

    public Task<TurnRoot> GetNextTurn(bool luckyStrike)
    {
        throw new System.NotImplementedException();
    }

    public Task<TurnRoot> GetFirstTurn(string card_id)
    {
        throw new System.NotImplementedException();
    }

    public Task<TurnRoot> GetNextTurn(string choice)
    {
        throw new System.NotImplementedException();
    }

    public Task<BattleCampaignRoot> GetAllTurns(string choice)
    {
        throw new System.NotImplementedException();
    }
}
