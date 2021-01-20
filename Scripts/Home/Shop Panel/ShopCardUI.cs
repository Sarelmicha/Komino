using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    [SerializeField] private Text headerText = null;
    [SerializeField] private Text descriptionText = null;
    [SerializeField] private Text amountText = null;
    [SerializeField] private Text costText = null;
    [SerializeField] private Image typeImage = null;
    [SerializeField] private Image costImage = null;

    public void SetCardDisplay(ShopCard shopCard)
    {
        print("inside SetCardDisplay");
        if (shopCard.shopCardData == null)
        {
            return;
        }

        // Set card text
        if (headerText != null)
        {
            if (shopCard.shopCardData.name == null)
            {
                headerText.gameObject.SetActive(false);
            }
            else
            {
                print("set header text to " + shopCard.shopCardData.name);
                headerText.text = shopCard.shopCardData.name;
            }
        }

        if (descriptionText != null)
        {
            print("set description text");
            //TODO - Change ask for amit from server
            descriptionText.text = "Rare";
        }

        if (amountText != null)
        {
            print("set amount text");
            if (shopCard.shopCardData.amount == 0)
            {
                amountText.gameObject.SetActive(false);

            }
            else
            {
                amountText.text = shopCard.shopCardData.amount.ToString();
            }
        }

        if (costText != null)
        {
            print("set cost text and type = " + shopCard.shopCardData.prizes);
            Debug.Log("cost is " + shopCard.shopCardData.cost);
            costText.text = shopCard.shopCardData.cost.ToString();
        }

        if (typeImage != null)
        {
            print("set type image");
            typeImage.sprite = shopCard.shopCardSprite;
        }

        if (costImage != null)
        {
            costImage.sprite = shopCard.currenceySprite;
        }

    }

    public void SetInteractableUI(bool interactable)
    {
        Image[] shopCardImages = GetComponentsInChildren<Image>();

        
        foreach (Image image in shopCardImages)
        {
            if (interactable)
            {
                image.color = Color.white;
            }

            else
            {
                image.color = Color.gray;
            }        
        }
    }
}
