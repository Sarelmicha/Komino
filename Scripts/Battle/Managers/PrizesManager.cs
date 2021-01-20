using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizesManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup allPrizes = null;
    [SerializeField] private PrizeCardLogic prizeCardLogicPrefab = null;

    public void InstantiatePrizesCards(List<PrizeCard> prizes)
    {
        print("prizes count = " + prizes.Count);
        foreach (PrizeCard prize in prizes)
        {
            PrizeCardLogic newPrizeCard = Instantiate(prizeCardLogicPrefab, allPrizes.transform.position, Quaternion.identity);
            newPrizeCard.SetPrizeCard(prize);
            newPrizeCard.transform.SetParent(allPrizes.transform);
        }
    }
}
