using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCardUI : MonoBehaviour
{

    [SerializeField] private Text valueText = null;
    [SerializeField] private Image prizeImage = null;

    public void SetCardDisplay(PrizeCard prizeCard)
    {
        print("inside SetCardDisplay");
        if (prizeCard.prizeData == null)
        {
            return;
        }

        if (valueText != null)
        {
            print("set amount text");
            valueText.text = prizeCard.prizeData.value.ToString();

        }

        if (prizeImage != null)
        {
            print("set type image");
            prizeImage.sprite = prizeCard.prizeCardSprite;
        }
    }
}
