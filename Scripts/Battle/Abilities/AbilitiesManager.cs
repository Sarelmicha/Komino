using Komino.CampaignBattle.Cards.BattleCards;
using Komino.CampaignBattle.Stats;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [SerializeField] private IntEvent OnBombChanceAbilityOccurred = null;
    [SerializeField] private TurnEvent OnCriticalHitAbilityOccured = null; 
    [SerializeField] private TurnEvent OnGiftAbilityOccurred = null;
    [SerializeField] private TurnEvent OnEnemySummonAbilityOccurred = null;

    public void InvokeAbility(ITurnManager turnManager, Turn turn,CampaignBattleCard battleCardController)
    {
       
        if (turn.card == null || turn.card.cast == null)
        { 
            //There is no ability to this card
            return ;
        }

        //if (turn.card.cast == AbilitiesConstants.BOMB_CHANCE)
        //{
        //    print("bomb chance ability was happened!");
        //    //Raise an event
        //    OnBombChanceAbilityOccurred.Raise(turn.bomb_chance);       
        //    return ;
        //}

        if (turn.card.cast == AbilitiesConstants.CRITICAL_HIT)
        {
  
            print("criticial hit ability was happened!");
            //Raise an event
            OnCriticalHitAbilityOccured.Raise(turn);


            return  ;
        }
        //if (turn.card.cast == AbilitiesConstants.DEFUSE)
        //{
        //    turnManager.HasDefuseAbility(true);
            
        //    return ;
        //}
        if (turn.card.cast == AbilitiesConstants.ENEMY_SUMMON)
        {
            print("enemy summon ability was happened!");
            OnEnemySummonAbilityOccurred.Raise(turn);
       
            return ;
        }

        if (turn.card.cast == AbilitiesConstants.GIFT)
        {
            print("GIFT  ability was happened!");
            OnGiftAbilityOccurred.Raise(turn);
            return ;
        }
        if (turn.card.cast == AbilitiesConstants.WIN)
        {
            print("win ability was happened!");
            //Set the card to be a winning card
            battleCardController.IsWinningCard(true);
            turnManager.HasWinAbility(true);
            return ;
        }

        if (turn.card.cast == AbilitiesConstants.JOKER)
        {
           //Set the card to be a joker card
            battleCardController.IsJoker(true);
            battleCardController.GetBattleCard().SetCreature("special","joker");
            battleCardController.GetBattleCardUI().SetCardDisplay(battleCardController.GetBattleCard());
            return ;           
        }

    }

}
