using UnityEngine;
using UnityEngine.UI;

public class UpgradeHUDManager : HUDManager
{
    [SerializeField] private Text numOfOwnedCards= null;
    [SerializeField] private Text numOfTotalCards = null;

    public void UpdateNumOfOwnedCards(int numOfOwnedCards, int numOfTotalCards)
    {
        this.numOfOwnedCards.text = numOfOwnedCards.ToString();
        this.numOfTotalCards.text = "/" + numOfTotalCards;
    }
}
