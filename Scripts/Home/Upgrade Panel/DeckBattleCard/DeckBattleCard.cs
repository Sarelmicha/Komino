using Komino.CampaignBattle.Cards.BattleCards;
using Komino.GameEvents.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBattleCard : MonoBehaviour
{
    [SerializeField] BattleCardEvent onDeckCardClicked = null;

    private BattleCard battleCard = null;
    private BattleCardUI battleCardUI = null;
    private Animator animator = null;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        battleCardUI = GetComponent<BattleCardUI>();
    }

    public BattleCardUI GetBattleCardUI()
    {
        return this.battleCardUI;

    }

    public BattleCard GetBattleCard()
    {
        return battleCard;
    }

    public void SetBattleCard(BattleCard battleCard)
    {
        this.battleCard = battleCard;

        // Update UI of cards after set the battle card
        battleCardUI.SetCardDisplay(battleCard);
    }

 
    public void OnDeckCardClicked()
    {
        onDeckCardClicked.Raise(battleCard);
    }

    public void ShowAsUnowned()
    {
        battleCardUI.GetCreatureImage().color = Color.black;
    }

}
