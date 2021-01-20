using Komino.CampaignBattle.Cards.BattleCards;
using Komino.Core;
using Komino.Enums;
using Komino.GameEvents.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Komino.CampaignBattle.Cards
{

    public class CardsHolder : MonoBehaviour
    {

        private Animator animator = null;
        private int nextEmptyPlaceHolderIndex = 0;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public Transform GetEmptySlot(bool isGameStarted)
        {

            print("nextEmptyPlaceHolderIndex is " + nextEmptyPlaceHolderIndex);

            GameObject cardHolder = transform.GetChild(nextEmptyPlaceHolderIndex++).gameObject;
          
            if (isGameStarted)
            {
               TriggerAddCardAnimation();
            }

            return cardHolder.transform;
        }

        public void TriggerAddCardAnimation()
        {
            // Reset the add card trigger for protection
            animator.ResetTrigger("addCard");
            // Set the trigger
            animator.SetTrigger("addCard");
        }

        public void MoveToState(int numOfCards)
        {
            print("im in moveToState and number of cards is " + numOfCards);


            animator.SetTrigger(numOfCards + "cards");
        }

        /*public void TriggerFightAnimation()
        {
            animator.SetTrigger("fight");
        }
        */

        public int GetNextEmptyPlaceHolderIndex()
        {
            return this.nextEmptyPlaceHolderIndex;
        }

        public void Reset()
        {
            nextEmptyPlaceHolderIndex = 0;
        }
    }
}
