using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeCardLogic : MonoBehaviour
{
    private Animator animator = null;
    private PrizeCardUI prizeCardUI = null;
    private PrizeCard prizeCard = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        prizeCardUI = GetComponent<PrizeCardUI>();
    }

    public PrizeCard GetPrizeCard()
    {
        return this.prizeCard;
    }

    public void SetPrizeCard(PrizeCard prizeCard)
    {
        this.prizeCard = prizeCard;

        // Update UI of cards after set the battle card
        prizeCardUI.SetCardDisplay(prizeCard);
    }
}
