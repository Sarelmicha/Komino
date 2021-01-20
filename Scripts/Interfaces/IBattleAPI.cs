using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IBattleAPI
{
    Task<BattleRoot> GetNewBattle();
    Task<TurnRoot> GetNextTurn(string choice);
    Task<BattleCampaignRoot> GetAllTurns(string choice);
    Task<AllTurns> GetActiveBattle();
    Task<GameStatusData> GetStatus(); 
}
