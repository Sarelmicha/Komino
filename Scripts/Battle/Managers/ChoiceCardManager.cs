using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCardManager : MonoBehaviour
{
    [SerializeField] VoidEvent onChoiceCardFinishedSummon = null;
    [SerializeField] CampaignChoiceCard campaignChoiceCard = null;
    [SerializeField] Transform choiceCardPlace = null;

    CampaignChoiceCard newCampaignChoiceCard = null;

    public void SummonChoiceCard(CardData cardData)
    {
        print("im in SummonChoiceCard");
        newCampaignChoiceCard  = Instantiate(campaignChoiceCard, choiceCardPlace.position, Quaternion.identity);
        newCampaignChoiceCard.SetNextCard(new ChoiceCard(cardData));
        newCampaignChoiceCard.transform.SetParent(choiceCardPlace);

        onChoiceCardFinishedSummon.Raise();
    }



    public void DestoryLastCampaignChoiceCard()
    {
        newCampaignChoiceCard.TriggerDisappearAnimation();
        Destroy(newCampaignChoiceCard.gameObject, newCampaignChoiceCard.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }


}
