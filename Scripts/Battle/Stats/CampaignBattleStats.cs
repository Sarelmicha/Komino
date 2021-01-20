using Komino.CampaignBattle.Cards.BattleCards;
using Komino.GameEvents.Events;
using ServerResponses.Player;
using System.Collections.Generic;
using UnityEngine;


namespace Komino.CampaignBattle.Stats
{
    [CreateAssetMenu(fileName = "CampaignBattleStats", menuName = "States/CampaignBattleStats")]
    public class CampaignBattleStats : ScriptableObject
    {


        [SerializeField] private ValueEvent OnPlayerScoreUpdated = null;
        [SerializeField] private ValueEvent OnEnemyScoreUpdated = null;

        [SerializeField] private ValueEvent OnAttackUpdated = null;
        [SerializeField] private ValueEvent OnAttackChanceUpdated = null;
        [SerializeField] private ValueEvent OnCritChanceUpdated = null;
        [SerializeField] private ValueEvent OnLootUpdated = null;
        //[SerializeField] private ValueEvent OnLuckChanceUpdated = null;
        [SerializeField] private ValueEvent OnAbilityUpdated = null;
        [SerializeField] private ValueEvent OnCurrentLootUpdated = null;
        [SerializeField] private ValueEvent OnMaxLootUpdated = null;



        private PlayerStats campaginBattlePlayerStats = new PlayerStats();
        private PlayerStats campaginBattleEnemyStats = new PlayerStats();
        private List<BattleCard> playerBattleCards = new List<BattleCard>();
        private List<LootData> lootBarsData = new List<LootData>();
        private List<BattleCard> enemyBattleCards = new List<BattleCard>();
        private TurnRoot lastTurn = null;
        private string currentBattleStatus = null;


        private double attackPerTurn = 0;
        private double lootPerTurn = 0;
        private double attackChancePerTurn = 0;
        private object abilityPerTurn = 0;
        private double critPerTurn = 0;
        private double maxTurns = 0;
        private double currentLoot = 0;
        private double maxLoot = 0;

        private int currentTargetLootDataIndex = 0;




        public double GetPlayerScore()
        {
            return this.campaginBattlePlayerStats.GetScore();
        }

        public void SetPlayerScore(double playerScore, bool setInstantly)
        {
            Debug.Log(playerScore);
            campaginBattlePlayerStats.SetScore(playerScore);
            Debug.Log("OnPlayerScoreUpdated has been raised!");
            OnPlayerScoreUpdated.Raise(new Value((int)playerScore, setInstantly));
        }
        public void SetCurrentLoot(double loot, bool setInstantly)
        {
            if (loot >= maxLoot)
            {
                loot = maxLoot;
            }

            this.currentLoot = loot;
            Debug.Log("OnCurrentLootUpdated has been raised!");
            OnCurrentLootUpdated.Raise(new Value((int)loot, setInstantly));
        }

        public double GetCurrentLoot()
        {
            return this.currentLoot;
        }

        public void SetMaxLoot(double loot, bool setInstantly)
        {
            this.maxLoot = loot;
            Debug.Log("OnMaxLootUpdated has been raised!");
            OnMaxLootUpdated.Raise(new Value((int)loot, setInstantly));
        }

        public double GetEnemyScore()
        {
            return this.campaginBattleEnemyStats.GetScore();
        }

        public void SetEnemyScore(double enemyScore, bool setInstantly)
        {
            this.campaginBattleEnemyStats.SetScore(enemyScore);
            Debug.Log("OnEnemyScoreUpdated has been raised!");
            OnEnemyScoreUpdated.Raise(new Value((int)enemyScore, setInstantly));
        }

        public void SetAttackPerTurn(double attackPerTurn, bool setInstantly)
        {
            this.attackPerTurn = attackPerTurn;
            Debug.Log("OnAttackUpdated has been raised!");
            OnAttackUpdated.Raise(new Value((int)attackPerTurn, setInstantly));
        }


        public void SetCritPerTurn(double critPerTurn, bool setInstantly)
        {
            this.critPerTurn = critPerTurn;
            Debug.Log("OnCritChanceUpdated has been raised!");
            OnCritChanceUpdated.Raise(new Value((int)critPerTurn, setInstantly));
        }

        public void SetAttackChancePerTurn(double attackChancePerTurn, bool setInstantly)
        {
            if (attackChancePerTurn == 0)
            {
                return;
            }

            this.attackChancePerTurn = attackChancePerTurn;
            Debug.Log("OnAttackChanceUpdated has been raised!");
            OnAttackChanceUpdated.Raise(new Value((int)attackChancePerTurn, setInstantly));
        }

        public void SetMaxTurns(double maxTurns)
        {
            this.maxTurns = maxTurns;
        }

        public void SetLootPerTurn(double lootPerTurn, bool setInstantly)
        {
            this.lootPerTurn = lootPerTurn;
            Debug.Log("OnLootUpdated has been raised!");
            OnLootUpdated.Raise(new Value((int)lootPerTurn, setInstantly));
        }

 

        public void SetAbilityPerTurn(object abilityPerTurn, bool setInstantly)
        {
            if (abilityPerTurn == null)
            {
                return;
            }
            this.abilityPerTurn = abilityPerTurn;
            Debug.Log("OnAbilityUpdated has been raised!");
            OnAbilityUpdated.Raise(new Value(-100, setInstantly));
        }


        public int GetPlayerCriticalHitDamage()
        {
            return this.campaginBattlePlayerStats.GetCriticalHitDamage();
        }


        public int GetPlayerCriticalHitChance()
        {
            return this.campaginBattlePlayerStats.GetCriticalHitChance();
        }

        public void SetLastTurn(TurnRoot lastTurn)
        {
            this.lastTurn = lastTurn;
        }

        public TurnRoot GetLastTurn()
        {
            return this.lastTurn;
        }

        public void SetCurrentBattleStatus(string currentBattleStatus)
        {
            this.currentBattleStatus = currentBattleStatus;
        }

        public string GetCurrentBattleStatus()
        {
            return this.currentBattleStatus;
        }

        public void ResetAllStats()
        {

            SetPlayerScore(0, true);
            SetEnemyScore(0, true);
            SetCurrentLoot(0, true);
            SetMaxLoot(0, true);
            //SetPlayerCriticalHitChance(0, true);
            //SetPlayerCriticalHitDamage(0, true);


            currentTargetLootDataIndex = 0;
            lootBarsData = null;

            RestartBotCards();
            RestartPlayerCards();



        }



        internal void SetLootBarsData(List<LootData> loot_bars)
        {
            this.lootBarsData = loot_bars;
        }

        public List<LootData> GetLootBarsData()
        {
            return this.lootBarsData;
        }

        public LootData GetCurrentLootTarget()
        {
            Debug.Log("currentTargetLootDataIndex = " + currentTargetLootDataIndex);
            Debug.Log("currentLootTarget is " + this.lootBarsData[currentTargetLootDataIndex].min);
            return this.lootBarsData[currentTargetLootDataIndex];
        }

        public void SetCurrentMaxLootDataIndex(int index)
        {
            this.currentTargetLootDataIndex = index;
        }

        public void SetCurrentMaxLootDataIndex(List<LootData> loot_bars, double currentLoot)
        {
            for(int i = 0; i  < loot_bars.Count - 1; i++)
            {
                Debug.Log("current loot is " + currentLoot);
                Debug.Log("loot_bars[i].min " + loot_bars[i].min);
                Debug.Log("loot_bars[i + 1].min " + loot_bars[i + 1].min);

                if (currentLoot >= loot_bars[i].min && currentLoot < loot_bars[i + 1].min)
                {
                    currentTargetLootDataIndex = i + 1;
                    break;
                }
            }
        }

        public void IncrementCurrentTargetLootDataIndex(int size)
        {
            if (currentTargetLootDataIndex == size)
            {
                return;
            }

            currentTargetLootDataIndex++;
        }

        public void SetPlayerBattleCards(List<BattleCard> battleCards)
        {
            this.playerBattleCards = battleCards;
        }

        public List<BattleCard> GetPlayerBattleCards()
        {
            return this.playerBattleCards;
        }

        public void SetEnemyBattleCards(List<BattleCard> battleCards)
        {
            this.enemyBattleCards = battleCards;
        }

        public List<BattleCard> GetEnemyBattleCards()
        {
            return this.enemyBattleCards;
        }

        public int GetNumOfPlayerCards()
        {
            return playerBattleCards.Count;
        }

        public int GetNumOfEnemyCards()
        {
            return enemyBattleCards.Count;
        }



        public void RestartBotCards()
        {
            enemyBattleCards.Clear();

        }

        public void RestartPlayerCards()
        {
            playerBattleCards.Clear();

        }

        public void ResetCards(string playerType)
        {
            if (playerType == "player")
            {
                RestartPlayerCards();
            }

            else
            {
                RestartBotCards();
            }
        }

        public double GetMaxTurns()
        {
            return maxTurns;
        }
    }
}

