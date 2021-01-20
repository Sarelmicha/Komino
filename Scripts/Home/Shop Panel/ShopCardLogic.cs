using Komino.GameEvents.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardLogic : MonoBehaviour
{
    private Animator animator = null;
    private ShopCardUI shopCardUI = null;
    private ShopCard shopCard = null;
    private Button shopCardButton = null;

    //Events
    [SerializeField] private ShopCardEvent onShopCardClicked = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        shopCardUI = GetComponent<ShopCardUI>();
        shopCardButton = GetComponent<Button>();
    }

    private void Start()
    {
        // if the card was already purchased, make the card uninteractable
        SetInteractable(!shopCard.shopCardData.purchased);
    }

    public ShopCard GetShopCard()
    {
        return this.shopCard;
    }

    public void SetShopCard(ShopCard shopCard)
    {
        this.shopCard = shopCard;

        // Update UI of cards after set the battle card
        shopCardUI.SetCardDisplay(shopCard);
    }

    public void OnShopCardClicked()
    {
        onShopCardClicked.Raise(this);
    }

    public void SetInteractable(bool interactable)
    {
        shopCardButton.interactable = interactable;
        shopCardUI.SetInteractableUI(interactable);
    }
}
