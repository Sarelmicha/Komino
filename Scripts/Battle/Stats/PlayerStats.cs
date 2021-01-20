using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private double score = 0;
    private int criticalHitDamage = 0;
    private int criticalHitChance = 0;
 
    public double GetScore()
    {
        return this.score;
    }

    public void SetScore(double score)
    {
        this.score = score;
    }

    public int GetCriticalHitDamage()
    {
        return this.criticalHitDamage;
    }

    public void SetCriticalHitDamage(int criticalHitDamage)
    {
        this.criticalHitDamage = criticalHitDamage;
    }

    public int GetCriticalHitChance()
    {
        return this.criticalHitChance;
    }

    public void SetCriticalHitChance(int criticalHitChance)
    {
        this.criticalHitChance = criticalHitChance;
    }





}
