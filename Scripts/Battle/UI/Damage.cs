using Komino.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    private PlayerType playerType;
    private string damageType;
    private int damage;

    public Damage(PlayerType playerType, string damageType, int damage)
    {
        this.playerType = playerType;
        this.damageType = damageType;
        this.damage = damage;
    }

    public PlayerType GetPlayerType()
    {
        return this.playerType;
    }

    public string GetDamageType()
    {
        return this.damageType;
    }

    public int GetDamage()
    {
        return this.damage;
    }
}
