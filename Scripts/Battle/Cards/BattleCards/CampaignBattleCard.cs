using Komino.Core;
using Komino.Enums;
using Komino.GameEvents.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Komino.CampaignBattle.Cards.BattleCards
{
    public class CampaignBattleCard : MonoBehaviour
    {
          
        //[SerializeField] float speed = 5f;
        [SerializeField] CampaignBattleCardEvent onPlayerCardFinishedSummon = null;
        [SerializeField] CampaignBattleCardEvent onEnemyCardFinishedSummon = null;
        [SerializeField] ParticlesStorage particlesStorage = null;

        private BattleCard battleCard = null;
        private BattleCardUI battleCardUI = null;

        private double amount = 1;

        private PlayerType playerType;
        private Transform target = null;
        private Animator animator = null;

        private bool isJoker = false;
        private bool isWinningCard = false;



        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            battleCardUI = GetComponent<BattleCardUI>();
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

        
    

        public void SetBattleCardRectTransform(Transform emptySlot)
        {            
            transform.SetParent(emptySlot);
            transform.eulerAngles = emptySlot.eulerAngles;
           
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void SetPlayerType(PlayerType playerType)
        {
            this.playerType = playerType;
        }

        public PlayerType GetPlayerType()
        {
            return this.playerType;
        }

        //public float GetSpeed()
        //{
        //    return this.speed;
        //}

        //public void SetSpeed(float speed)
        //{
        //   this.speed = speed;
        //}

        public double GetAmount()
        {
            return this.amount;
        }

        public void SetAmount(double amount)
        {
            this.amount = amount;
  
        }

        public BattleCardUI GetBattleCardUI()
        {
            return this.battleCardUI;
            
        }


        public void IsJoker(bool isJoker)
        {
            this.isJoker = isJoker;
        }


        public bool IsJoker()
        {
            return this.isJoker;
        }

        public void IsWinningCard(bool isWinningCard)
        {
            this.isWinningCard = isWinningCard;
        }


        public bool IsWinningCard()
        {
            return this.isWinningCard;
        }

        public void TriggerFlipWithNoScaleAnimation()
        {
            animator.SetTrigger("flipNoScale");
        }

        public void TriggerFlipWithScaleAnimation()
        {
            animator.SetTrigger("flipScale");
        }

        public void TriggerBotFlipBotAnimation()
        {
            animator.SetTrigger("botFlip");
        }


        public void TriggerIdleAnimation()
        {
            animator.SetTrigger("finishSummon");
        }

        public void TriggerOpenNoFlipAnimation()
        {
            animator.SetTrigger("open");
        }

        public void TriggerCloseAnimation()
        {
            animator.SetTrigger("close");
        }
    }
}
