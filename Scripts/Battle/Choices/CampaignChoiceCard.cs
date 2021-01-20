using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignChoiceCard : MonoBehaviour
{
    private ChoiceCard nextCard = null;
    private ChoiceCardUI nextCardUI = null;
    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        nextCardUI = GetComponent<ChoiceCardUI>();
    }

    public void SetNextCard(ChoiceCard nextCard)
    {
        this.nextCard = nextCard;
        // Update UI of cards after set the battle card
        nextCardUI.SetCardDisplay(nextCard);
    }

    public void TriggerDisappearAnimation()
    {
        animator.SetTrigger("disappear");
    }
}
