using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrebattleHUDManager : HUDManager
{
    [SerializeField] private Text playerDeckPowerText = null;
    [SerializeField] private Text enemyDeckPowerText = null;



    public void SetPlayerDeckPowerText(int deckPower)
    {
        playerDeckPowerText.text = deckPower.ToString();
    }

    public void SetEnemyDeckPowerText(int deckPower)
    {
        enemyDeckPowerText.text = deckPower.ToString();
    }
}
