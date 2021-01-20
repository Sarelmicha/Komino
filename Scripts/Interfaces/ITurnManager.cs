using Komino.CampaignBattle.Cards.BattleCards;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnManager
{
    void HandleOneTurn(Turn turn, string choice,List<BattleCard> battleCards);
    void SetCards(List<Turn> player_turns);
    void RestartGame();
    void DestroyLastCard();
    void RestartBoard();
    void HasDefuseAbility(bool hasDefuseAbility);
    bool HasDefuseAbility();
    void HasWinAbility(bool hasWinAbility);
    bool HasWinAbility();

}
