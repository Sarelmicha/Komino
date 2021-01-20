using Komino.Enums;
using Komino.GameEvents.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    [SerializeField] private DamageEvent onComboOccures = null;
    [SerializeField] private DamageEvent onCriticalHitOccures = null;

    public bool HandleCombo(double amount, PlayerType playerType, double damage)
    {
        print("im here playerType is " + playerType + "and amount is " + amount);

        if (amount <= 1)
        {
            return false;
        }

        switch (amount)
        {
            case 2:
                // Raise an event that combo has accourd
                print("onComboOccures has been raised!");
                onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.DOUBLE_COMBO, (int)damage));
                return true;
            case 3:
                print("onComboOccures has been raised!");
                onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.TRIPLE_COMBO, (int)damage));
                return true;
            case 4:
                print("onComboOccures has been raised!");
                onComboOccures.Raise(new Damage(playerType, AnnouncerConstants.QUADRO_COMBO, (int)damage));
                return true;
        }

        return false;

    }

    public bool HandleCriticalHit(PlayerType playerType, int damage)
    {

        if (damage > 0)
        {
            print("onCriticalHitOccures has been raised sss!");
            onCriticalHitOccures.Raise(new Damage(playerType, AnnouncerConstants.CRITICAL_HIT, damage));
            //Critical hit has occured
            return true;
        }
        // Critical hit has not occured
        return false;
    }
}
